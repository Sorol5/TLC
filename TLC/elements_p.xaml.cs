﻿using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для elements_p.xaml
    /// </summary>
    public partial class elements_p : Page
    {
        public elements_p()
        {
            InitializeComponent();
        }
        void Items_p(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("items_p.xaml", UriKind.Relative));
        }
    }
}
