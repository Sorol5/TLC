using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using TLCBibliary;
using static System.Windows.Forms.LinkLabel;
using static TLC.Parameters;

namespace TLC
{
    /// <summary>
    /// Логика взаимодействия для element_p.xaml
    /// </summary>
    public partial class element_p : Page
    {
        public string elementDirectory = null;
        public static ObservableCollection<Element> parametersList = new ObservableCollection<Element>();
        private List<Element> deletedParameters = new List<Element>();
        public static int defaultParams = 0;

        public element_p()
        {
            InitializeComponent();
            string modDirectory = (string)App.Current.Resources["ModDirectory"];
            string elementName = (string)App.Current.Resources["ElementName"];
            elementDirectory = $@"{modDirectory}\Content\Items\{elementName}";
            string[] lines = File.ReadAllLines(elementDirectory);
            string paramDirectory = @"C:\Users\chmoc\source\repos\TLC\TLC\Resources\Parameters.txt";
            string[] p_lines = File.ReadAllLines(paramDirectory);
            itemsView.ItemsSource = parametersList;
            GetParameter(lines, "Item.");
            foreach (var line in p_lines)
            {
                var parts = line.Split(';');
                for(int i = 0; i < parametersList.Count; i++)
                {
                    if (parts[0] == parametersList[i].Parameter & parts.Length == 3)
                    {
                        parametersList[i].Description = parts[2];
                    }
                }
            }
        }

        public static void GetParameter(string[] lines, string parameter)
        {
            parametersList.Clear();
            string valuePattern = $"{Regex.Escape(parameter)}(.*){Regex.Escape(" = ")}";
            string replacement = "";

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(parameter))
                {
                    string oldValue = lines[i];
                    string par = lines[i];
                    string value = oldValue;
                    value = Regex.Replace(value, valuePattern, replacement);
                    value = string.Join(" ", value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim()));
                    value = value.Replace(";", "");
                    oldValue = value;
                    par = string.Join(" ", par.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim()));
                    par = par.Replace(value, "");
                    par = par.Replace(";", "");
                    parametersList.Add(new Element { Parameter = par, Value = value, oldValue = oldValue });
                    defaultParams++;
                }
            }
        }
        void Save(object sender, RoutedEventArgs e)
        {
            foreach (var deletedParameter in deletedParameters)
            {
                RemoveParameterFromFile(elementDirectory, deletedParameter);
            }
            int GetLineNumber(string pathToFile, string content)
            {
                int lineNumber = 0;
                foreach (var line in File.ReadAllLines(pathToFile))
                {
                    lineNumber++;
                    if (line.Contains(content))
                    {
                        return lineNumber;
                    }
                }
                return lineNumber;
            }
            int num = GetLineNumber(elementDirectory, parametersList[defaultParams-1].Parameter);
            for (int i = 0; i < parametersList.Count; i++)
            {
                if (parametersList[i].oldValue == null)
                {
                    string text = parametersList[i].Parameter + parametersList[i].Value;
                    Files.TextReplace(elementDirectory, text, num);
                    parametersList[i].oldValue = parametersList[i].Value;
                    num++;
                }
                else
                {
                    Files.TextReplace(elementDirectory, parametersList[i].oldValue, parametersList[i].Value);
                }
            }
            deletedParameters.Clear();
        }
        private void RemoveParameterFromFile(string filePath, Element parameter)
        {
            var lines = File.ReadAllLines(filePath).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains(parameter.Parameter) & lines[i].Contains(parameter.Value))
                {
                    lines.RemoveAt(i);
                    break;
                }
            }
            File.WriteAllLines(filePath, lines);
        }
        void New(object sender, RoutedEventArgs e)
        {
            var listWindow = new Parameters();
            listWindow.ShowDialog();
            var param = (Parameter)App.Current.Resources["ParamList"];
            if(param != null)
            {
                parametersList.Add(new Element { Parameter = param.Param, Value = param.Value, Description = param.Description });
            }
            //parametersList.Add();
            //App.Current.Resources["ElementName"];
        }
        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                // Получаем элемент, который нужно удалить
                Element itemToRemove = button.Tag as Element;
                if (itemToRemove != null)
                {
                    // Удаляем элемент из коллекции
                    parametersList.Remove(itemToRemove);
                    deletedParameters.Add(itemToRemove);
                    defaultParams--;

                }
            }
        }
        public class Element
        {
            public string Parameter { get; set; }
            public string Value { get; set; }
            public string oldValue { get; set; }
            public string Description { get; set; }
        }
    }
}
