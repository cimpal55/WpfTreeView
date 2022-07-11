using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;

namespace WpfTreeView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        
        #endregion

        #region OnLoad
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                // Create a new item for it 
                var item = new TreeViewItem
                {
                    // The header and  full path
                    Header = drive,
                    Tag = drive
                };

                // Listen out for item being expanded
                item.Expanded += Folder_Expanded;
                
                // Dummy item
                item.Items.Add(null);

                // Add it to the main tree-view 
                FolderView.Items.Add(item);

            }
        }

        #endregion

        #region Folder Expanded
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks

            var item = (TreeViewItem)sender;

            // If the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            // Clear dummy data
            item.Items.Clear();

            // Get full path
            var fullPath = (string)item.Tag;

            #endregion

            #region Get Directories

            // Create a blank list for directories
            var directories = new List<string>();

            // Try and get directories from the folder (ignoring issues)
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            // For each directory
            directories.ForEach(directoryPath =>
            {
                // Create a directory item
                var subItem = new TreeViewItem()
                {
                    // Header as a folder name
                    Header = GetFileFolderName(directoryPath),
                    // Tag as full path
                    Tag = directoryPath
                };

                // Dummy item for expanding folder
                subItem.Items.Add(null);

                subItem.Expanded += Folder_Expanded;

                // Add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files
            // Create a blank list for files
            var files = new List<string>();

            // Try and get directories from the folder (ignoring issues)
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch { }

            // For each directory
            files.ForEach(filePath =>
            {
                // Create a directory item
                var subItem = new TreeViewItem()
                {
                    // Header as a file name
                    Header = GetFileFolderName(filePath),
                    // Tag as full path
                    Tag = filePath
                };

                // Add this item to the parent
                item.Items.Add(subItem);
            });
            #endregion
        }





        #endregion

        #region Helpers

        /// <summary>
        /// Find the folder or file name from a full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var normalizedPath = path.Replace('/', '\\');

            var lastIndex = normalizedPath.LastIndexOf('\\');

            // If we don't find a backslash, return the path itself
            if (lastIndex <= 0)
                return path;

            // Return the path after backslash 
            return path.Substring(lastIndex + 1);

        }

        #endregion

    }
}
