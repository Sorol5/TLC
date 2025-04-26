using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TLCBibliary;

namespace TLC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void Create_project(object sender, RoutedEventArgs e)
        {
            Pages.Navigate(new Uri("Create_project.xaml", UriKind.Relative));
        }
        void Open_project(object sender, RoutedEventArgs e)
        {
            string directory = null;
            directory = Paths.SelectDir();
            if (directory != null)
            {
                string[] mNameCs = Directory.GetFiles($@"{directory}", "*.csproj");
                if (mNameCs.Length != 0)
                {
                    App.Current.Resources["ModDirectory"] = directory;
                    Pages.Navigate(new Uri("Project.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Мод не обнаружен");
                }

            }
        }
    }
}
