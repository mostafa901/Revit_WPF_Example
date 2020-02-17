using System;
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
using System.Windows.Shapes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revit_WPF_Example
{
    /// <summary>
    /// Interaction logic for WPF_Window.xaml
    /// </summary>
    public partial class WPF_Window: Window
    {
        public WPF_Window(UIApplication uiApp)
        {
            InitializeComponent();
            var mv_DataContext = new ModelView.MV_Window(uiApp);
            SizeToContent = SizeToContent.Width;
            DataContext = mv_DataContext;
        }       
    }
}
