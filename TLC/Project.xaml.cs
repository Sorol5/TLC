using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace TLC
{
    /// <summary>
    /// Логика взаимодействия для Project.xaml
    /// </summary>
    public partial class Project : Page
    {
        public Project()
        {
            InitializeComponent();
            if (App.Current.Resources["ModDirectory"] != null)
            {
                URL.Text = (string)App.Current.Resources["ModDirectory"];
            }
            string modDirectory = (string)App.Current.Resources["ModDirectory"];
            string[] mNameCs = Directory.GetFiles($@"{modDirectory}", "*.csproj");
            App.Current.Resources["ModName"] = System.IO.Path.GetFileNameWithoutExtension(mNameCs[0]);
        }

        private void Main_p(object sender, RoutedEventArgs e)
        {
            Pages.Navigate(new Uri("main_p.xaml", UriKind.Relative));
        }
        private void Elements_p(object sender, RoutedEventArgs e)
        {
            Pages.Navigate(new Uri("elements_p.xaml", UriKind.Relative));
        }
    }
}
