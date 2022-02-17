using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirstFloor.ModernUI.Presentation;

namespace M2BCenter.Model
{
    public class PreLoadModel : NotifyPropertyChanged
    {
        private string _name;
        private bool _checked = false;
        private string _error;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                this.OnPropertyChanged("Checked");
            }
        }

        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                this.OnPropertyChanged("Error");
            }
        }


    }
}
