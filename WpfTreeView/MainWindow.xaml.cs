using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WpfTreeView.MVVM.ViewModel;
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

            this.DataContext = new DirectoryStructureViewModel(); 
        }
        
        #endregion

    }
}
