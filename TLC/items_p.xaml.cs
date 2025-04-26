using System;
using System.IO;
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
using System.Windows.Forms;
using TLCBibliary;
using static System.Net.Mime.MediaTypeNames;
using static TLC.element_p;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace TLC
{
    /// <summary>
    /// Логика взаимодействия для items_p.xaml
    /// </summary>
    public partial class items_p : Page
    {
        public static ObservableCollection<Item> itemsList = new ObservableCollection<Item>();
        public items_p()
        {
            InitializeComponent();
            itemsList.Clear();
            string modDirectory = (string)App.Current.Resources["ModDirectory"];
            string[] cs_items = Directory.GetFiles($@"{modDirectory}\Content\Items", "*.cs");
            string[] img_items = Directory.GetFiles($@"{modDirectory}\Content\Items", "*.png");
            for(int i = cs_items.Length; i > 0; i--)
            {
                FileInfo fileInfo = new FileInfo(cs_items[i - 1]);
                string name = fileInfo.Name;
                name.Replace(".cs", "");
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(img_items[i - 1]);
                image.EndInit();
                itemsList.Add(new Item { Name = name, Image = image });
                itemsView.ItemsSource = itemsList;
            }
        }
        public class Item
        {
            public string Name { get; set; }
            public BitmapImage Image { get; set; }
        }

        private void ItemClick(object sender, MouseButtonEventArgs e)
        {
            if (itemsView.SelectedItem != null)
            {
                var selectedItem = (Item)itemsView.SelectedItem;
                App.Current.Resources["ElementName"] = selectedItem.Name;
                this.NavigationService.Navigate(new Uri("element_p.xaml", UriKind.Relative));
            }
        }
        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                // Получаем элемент, который нужно удалить
                Item itemToRemove = button.Tag as Item;
                if (itemToRemove != null)
                {
                    // Удаляем элемент из коллекции
                    itemsList.Remove(itemToRemove);
                    System.IO.File.Delete($@"{(string)App.Current.Resources["ModDirectory"]}\Content\Items\{itemToRemove.Name}");
                    System.IO.File.Delete($@"{(string)App.Current.Resources["ModDirectory"]}\Content\Items\{itemToRemove.Name.Substring(0, itemToRemove.Name.Length - 3) + ".png"}");
                }
            }
        }
        void New_item(object sender, RoutedEventArgs e)
        {
            string exMod = "C:\\Users\\chmoc\\source\\repos\\TLC\\TLC\\Resources\\NewElements\\ExampleElement\\";
            string mName = (string)App.Current.Resources["ModName"];
            string mDirectory = $@"{(string)App.Current.Resources["ModDirectory"]}\Content\Items";
            Paths.CopyDirectory(exMod, mDirectory, true);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(($@"{mDirectory}\elementName.png"));
            image.EndInit();
            itemsList.Add(new Item { Name = "elementName.cs", Image = image });
            Files.TextReplace($@"{mDirectory}\elementName.cs", "modName", mName);
        }
    }
}
