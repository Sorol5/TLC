using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TLCBibliary;
using static System.Net.Mime.MediaTypeNames;

namespace TLC
{
    /// <summary>
    /// Логика взаимодействия для main_p.xaml
    /// </summary>
    public partial class main_p : Page
    {
        public string modDirectory = (string)App.Current.Resources["ModDirectory"];
        public string mName, mDisplayName, author, description, imgDirectory = null;
        private static readonly ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
        public main_p()
        {
            InitializeComponent();
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri($@"{modDirectory}\icon.png");
            image.EndInit();
            img.Source = image;
            string[] mNameCs = Directory.GetFiles($@"{modDirectory}", "*.csproj");
            mName = Path.GetFileNameWithoutExtension(mNameCs[0]);
            mDisplayName = Files.GetTextFromFile($@"{modDirectory}\build.txt", "displayName", "displayName = ");
            author = Files.GetTextFromFile($@"{modDirectory}\build.txt", "author", "author = ");
            description = File.ReadAllText($@"{modDirectory}\description.txt");
            modName.Text = mName;
            modDisplayName.Text = mDisplayName;
            modAuthor.Text = author;
            Description.Text = description;

        }
        void Save(object sender, RoutedEventArgs e)
        {
            string new_mName = modName.Text;
            string new_mDisplayName = modDisplayName.Text;
            string new_author = modAuthor.Text;
            string new_description = Description.Text;
            string[] files = Directory.GetFiles(modDirectory, "*.*", SearchOption.AllDirectories);
            string[] previous = { mName, mDisplayName, author};
            string[] after = { new_mName, new_mDisplayName, new_author};
            string new_modDirectory = modDirectory.Replace(mName, new_mName);
            Files.TextReplace(files, previous, after);
            FileStream f_description = new FileStream($@"{modDirectory}\description.txt", FileMode.Create, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(f_description);
            streamWriter.Write(new_description);
            streamWriter.Close();
            f_description.Close();
            FileInfo newIcon = new FileInfo(imgDirectory);
            if (newIcon.Exists)
            {
                newIcon.CopyTo($@"{modDirectory}\icon.png", true);
            }
            File.Move($@"{modDirectory}\{mName}.csproj", $@"{modDirectory}\{new_mName}.csproj");
            File.Move($@"{modDirectory}\{mName}.cs", $@"{modDirectory}\{new_mName}.cs");
            Directory.Move(modDirectory, new_modDirectory);
            App.Current.Resources["ModDirectory"] = new_modDirectory;
        }
        void Image_Select(object sender, RoutedEventArgs e)
        {
            imgDirectory = Paths.SelectImg();
            if(imgDirectory != null)
            {
                img.Source = (ImageSource)imageSourceConverter.ConvertFromString(imgDirectory);
            }
        }
    }
}
