using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TLCBibliary
{
    public class Paths
    {
        static public string SelectDir()
        {
            string directory = null;
            var folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                directory = folderBrowser.SelectedPath;
                return directory;
            }
            else 
            {
                return directory;
            }
        }
        static public string SelectImg()
        {
            string directory = null;
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                directory = fileDialog.FileName;
                return directory;
            }
            else
            {
                return directory;
            }
        }
        static public void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = System.IO.Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = System.IO.Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }
    }
    public class Files
    {
        public static void TextReplace(string file, string previous, string after) 
        {
            string text = File.ReadAllText(file);
            text = text.Replace(previous, after);
            File.WriteAllText(file, text);
        }
        public static void TextReplace(string file, string text, int last)
        {
            var lines = File.ReadAllLines(file);
            var updatedLines = new List<string>(lines);
            string GetIndentation(string line)
            {
                // Получаем количество пробелов или табуляций в начале строки
                int i = 0;
                while (i < line.Length && (line[i] == ' ' || line[i] == '\t'))
                {
                    i++;
                }
                return line.Substring(0, i);
            }
            string indent = GetIndentation(lines[last-1]);
            updatedLines.Insert(last, indent + text + ";");
            File.WriteAllLines(file, updatedLines);
        }
        public static void TextReplace(string[] files, string[] previous, string[] after)
        {
            for (int i = files.Length; i > 0; i--)
            {
                if (!files[i - 1].EndsWith(".png"))
                {
                    string text = File.ReadAllText(files[i - 1]);
                    for(int a = previous.Length; a > 0; a--)
                    {
                        text = text.Replace(previous[a - 1], after[a - 1]);
                    }
                    File.WriteAllText(files[i - 1], text);
                }
            }
        }
        public static string GetTextFromFile(string path, string usefulText, string uselessText)
        {
            string text = GetString(GetLineNumber(path, usefulText) - 1, path);
            text = text.Replace(uselessText, "");
            string GetString(int numberString, string filePath)
            {
                IEnumerable<string> result = File.ReadLines(filePath).Skip(numberString).Take(1);

                string newString = null;

                foreach (string str in result)
                {
                    newString += str;
                }

                return newString;
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
            return text;
        }
    }
}
