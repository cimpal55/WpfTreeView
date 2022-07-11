using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfTreeView
{
    /// <summary>
    /// Converts a full path to a specific image type of a drive, folder or file
    /// </summary>

    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImage : IValueConverter
    {
        public static HeaderToImage Instance = new HeaderToImage();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Get the full path
            var path = (string)value;

            if (path == null)
                return null;
            
            // Get the name of the file/folder
            var name = MainWindow.GetFileFolderName(path);

            // By default, we resume an image
            var image = "Images/file.png";

            if (string.IsNullOrEmpty(name))
                image = "Images/drive.png";
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
                image = "Images/folder.png";


            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
