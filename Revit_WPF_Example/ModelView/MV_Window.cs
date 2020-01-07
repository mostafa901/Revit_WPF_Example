using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Revit_WPF_Example.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Revit_WPF_Example.ModelView
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged?view=netframework-4.8
    public class MV_Window: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Document Doc { get; set; }
        private UIDocument UIDoc { get; set; }

        private UIApplication uIApp;

        public UIApplication GetUIApp()
        {
            return uIApp;
        }

        internal void SetUIApp(UIApplication value)
        {
            uIApp = value;
            UIDoc = uIApp.ActiveUIDocument;
            Doc = UIDoc.Document;
        }

        #region SelectedElement

        private string _SelectedElement;

        public string SelectedElement

        {
            get
            {
                return _SelectedElement;
            }
            set
            {
                _SelectedElement = value;
                NotifyPropertyChanged(nameof(SelectedElement));
            }
        }

        #endregion SelectedElement

        #region CMD_Select

        private CustomCommand _CMD_Select;

        public CustomCommand CMD_Select
        {
            get
            {
                if (_CMD_Select == null) _CMD_Select = new CustomCommand();
                return _CMD_Select;
            }
            set
            {
                _CMD_Select = value;
                NotifyPropertyChanged(nameof(CMD_Select));
            }
        }

        #endregion CMD_Select

        private ExternalEvent RvtExEvent;
        private Executer ExecuterHandler;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MV_Window()
        {
            SetupRevitEventHandler();
            SetupCommands();
        }

        private void SetupRevitEventHandler()
        {
            // A new handler to handle request posting by the dialog
            ExecuterHandler = new Executer();
            RvtExEvent = ExternalEvent.Create(ExecuterHandler);
        }

        private void SetupCommands()
        {
            CMD_Select.CommandAction = () =>
            {
                RunOnREvitThread(() =>
                {
                    try
                    {
                        var refEle = UIDoc.Selection.PickObject(ObjectType.Element);
                        var element = Doc.GetElement(refEle);
                        SelectedElement = $"You Selected {element.Name} of Id: {element.Id}";
                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show("WPF Window Test", "User Aborted");
                    }
                });
            };
        }

        private bool RunOnREvitThread(Action action)
        {
            ExecuterHandler.ExcutableAction = action;
            RvtExEvent.Raise();
            return true;
        }
    }
}