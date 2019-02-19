using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace LockMobileClient.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public ICommand RegisternCmd { get; }

        string _secretCode;
        public string SecretCode
        {
            get { return _secretCode; }
            set
            {
                if (_secretCode != value)
                {
                    _secretCode = value;
                    OnPropertyChanged("SecretCode");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
