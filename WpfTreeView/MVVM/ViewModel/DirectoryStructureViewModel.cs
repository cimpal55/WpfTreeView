using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTreeView.MVVM.ViewModel
{
    /// <summary>
    /// The view model for the applications main directory view
    /// </summary>
    public class DirectoryStructureViewModel : RelayCommands
    {
        #region Public Properties
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            // Get the logical drives
            var children = DirectoryStructure.GetLogicalDrives();

            // Create the view models from the data
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
        }

        #endregion

    }
}
