using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WK.Libraries.BetterFolderBrowserNS;
using TLCBibliary;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace TLC
{
    public partial class Create_project : Page
    {
        public string directory = @"C:\\";
        //public string curdir = AppDomain.CurrentDomain.BaseDirectory;
        //public string image = $@"{AppDomain.CurrentDomain.BaseDirectory}\Resources\Default.png";
        public string image = @"C:\Users\chmoc\source\repos\TLC\TLC\\Resources\Default.png";
        private static readonly ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
        public Create_project()
        {
            InitializeComponent();
        }
        private void Dir_select(object sender, RoutedEventArgs e)
        {
            directory = Paths.SelectDir();
            if (directory != null)
            {
                dir.Content = directory;
            }
        }
        private void Image_select(object sender, RoutedEventArgs e)
        {
            image = Paths.SelectImg();
            if (image != null)
            {
                img.Source = (ImageSource)imageSourceConverter.ConvertFromString(image);
            }
        }
        private void Create(object sender, RoutedEventArgs e)
        {
            string exMod = "C:\\Users\\chmoc\\source\\repos\\TLC\\TLC\\Resources\\NewProject\\ExampleMod";
            //string exMod = $"{curdir}\\Resources\\NewProject\\ExampleMod";
            string mName = modName.Text;
            App.Current.Resources["ModName"] = modName.Text;
            string mDisplayName = modDisplayName.Text;
            string mAuthor = modAuthor.Text;
            string des = description.Text;
            Paths.CopyDirectory(exMod, directory, true);
            Directory.Move($"{directory}\\modName", $"{directory}\\{mName}");
            File.Move($"{directory}\\{mName}\\modName.cs", $"{directory}\\{mName}\\{mName}.cs");
            File.Move($"{directory}\\{mName}\\modName.csproj", $"{directory}\\{mName}\\{mName}.csproj");
            FileInfo newIcon = new FileInfo(image);
            if (newIcon.Exists)
            {
                newIcon.CopyTo($"{directory}\\{mName}\\icon.png", true);
            }
            string[] files = Directory.GetFiles($"{directory}\\{mName}", "*.*", SearchOption.AllDirectories);
            string[] previous = { "modName", "modDisplayName","modAuthor"};
            string[] after = { mName, mDisplayName, mAuthor };
            Files.TextReplace(files, previous, after);
            File.WriteAllText($"{directory}\\{mName}\\description.txt", des);
            App.Current.Resources["ModDirectory"] = $@"{directory}\{mName}";
            this.NavigationService.Navigate(new Uri("Project.xaml", UriKind.Relative));
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            
        }
    }
    
}
