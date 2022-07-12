using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfTreeView
{
    public class DirectoryItemViewModel : RelayCommands
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">The full path of this item</param>
        /// <param name="type">The type of item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            // Create commands
            ExpandCommand = new RelayCommand(Expand);

            FullPath = fullPath;
            Type = type;

            ClearChildren();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The full path to the item
        /// </summary>
        public string FullPath { get; set; }

        public string Name => this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath);


        /// <summary>
        /// A list of all children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children;

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand { get => this.Type != DirectoryItemType.File; }

        /// <summary>
        /// Indicates if the current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                // If the UI tells us to expand
                if (value == true)
                    // Find all Children
                    Expand();
                else
                    this.ClearChildren();
            }
        }
        public ICommand ExpandCommand { get; set; }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Removes all children from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren() 
        {
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            // Show the expand arrow if we are not a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        #endregion

        /// <summary>
        /// Expands this directory and finds all children
        /// </summary>
        private void Expand() {
            if (this.Type == DirectoryItemType.File)
                return;

            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);

            // Find all children
            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }
}
