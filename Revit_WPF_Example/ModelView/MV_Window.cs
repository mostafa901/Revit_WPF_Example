using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Revit_WPF_Example.Commands;
using Revit_WPF_Example.Core;
using Revit_WPF_Example.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private RevitActions RvtActions;

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

        #region Items

        private ObservableCollection<ElementModel> _Items;

        public ObservableCollection<ElementModel> Items
        {
            get
            {
                if (_Items == null) _Items = new ObservableCollection<ElementModel>();
                return _Items;
            }
            set
            {
                _Items = value;
                NotifyPropertyChanged(nameof(Items));
            }
        }

        #endregion Items

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MV_Window(UIApplication uiApp)
        {
            SetupCommands();
            RvtActions = new RevitActions(uiApp);
            CollectWallElements();
        }

        private void CollectWallElements()
        {
            var filcol = new FilteredElementCollector(RvtActions.Doc).OfClass(typeof(Wall));
            foreach (var wall in filcol)
            {
                ElementModel model = new ElementModel();
                model.Name = $"{wall.Name}: {wall.Id.IntegerValue}";
                model.CMD = new CustomCommand();
                model.CMD.CommandAction = () =>
                {
                    RunOnREvitThread(() =>
                    {
                        var scope = new ScopeToElement(RvtActions);
                    scope.ScopeToRvtElement(wall);
                    });
                };
                Items.Add(model);
            }
        }

       
   

    private void SetupCommands()
        {
            CMD_Select.CommandAction = () =>
            {
                RunOnREvitThread(() => SelectedElement = RvtActions.SelectElement());
            };
        }

        private bool RunOnREvitThread(Action action)
        {
            RvtActions.ExecuterHandler.ExcutableAction = action;
            RvtActions.RvtExEvent.Raise();
            return true;
        }
    }
}