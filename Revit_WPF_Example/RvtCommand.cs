using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_WPF_Example
{
    [Transaction(TransactionMode.Manual)]
    public class RvtCommand: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var win = new WPF_Window(commandData.Application);
            win.Show();

            return Result.Succeeded;
        }
    }
}