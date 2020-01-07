using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Revit_WPF_Example.Commands
{
    internal class Executer: IExternalEventHandler
    {
        public Action ExcutableAction;

        public void Execute(UIApplication app)
        {
            try
            {
                ExcutableAction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetName()
        {
            return "WPF Test";
        }
    }
}