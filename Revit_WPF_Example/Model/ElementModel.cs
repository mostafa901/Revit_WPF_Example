using Revit_WPF_Example.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Revit_WPF_Example.Model
{
    public class ElementModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Name

        private string _Name;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        #endregion Name



        public CustomCommand CMD { get; set; }



    }
}