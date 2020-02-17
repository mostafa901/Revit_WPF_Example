using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Revit_WPF_Example.Commands;

namespace Revit_WPF_Example.Core
{
    class RevitActions
    {
        public Document Doc { get; set; }
        public UIDocument UIDoc { get; set; }
        private UIApplication uIApp;
       
        public ExternalEvent RvtExEvent;
        public Executer ExecuterHandler;

        public RevitActions(UIApplication uiApp)
        {
            SetUIApp(uiApp);
            SetupRevitEventHandler();

        }

        private void SetupRevitEventHandler()
        {
            // A new handler to handle request posted by the dialog
            ExecuterHandler = new Executer();
            RvtExEvent = ExternalEvent.Create(ExecuterHandler);
        }

        private void SetUIApp(UIApplication value)
        {
            uIApp = value;
            UIDoc = uIApp.ActiveUIDocument;
            Doc = UIDoc.Document;
        }
       

        public string SelectElement(){
            try
            {
                var refEle = UIDoc.Selection.PickObject(ObjectType.Element);
                var element = Doc.GetElement(refEle);
                return $"You Selected {element.Name} of Id: {element.Id}";
            }
            catch (Exception ex)
            {
                TaskDialog.Show("WPF Window Test", "User Aborted");
                return "";
            }
        }
    }
}
