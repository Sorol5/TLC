using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using static TLC.element_p;

namespace TLC
{
    /// <summary>
    /// Логика взаимодействия для Parameters.xaml
    /// </summary>
    public partial class Parameters : Window
    {
        public static List<Parameter> parametersList = new List<Parameter>();
        public Parameters()
        {
            InitializeComponent();
            string elementDirectory = @"C:\Users\chmoc\source\repos\TLC\TLC\Resources\Parameters.txt";
            string[] lines = File.ReadAllLines(elementDirectory);
            ListView.ItemsSource = GetParameter(lines);
        }
        public class Parameter
        {
            public string Param { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }

        }
        public static List<Parameter> GetParameter(string[] lines)
        {
            parametersList.Clear();
            foreach (var line in lines)
            {
                var parts = line.Split(';');

                if (parts.Length == 3)
                {
                    parametersList.Add(new Parameter { Param = parts[0], Value = parts[1], Description = parts[2] });
                }
            }
            return parametersList;
        }
        private void ValuesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Parameter)ListView.SelectedItem;
            App.Current.Resources["ParamList"] = selectedItem;
            this.Close();
        }
    }
}
