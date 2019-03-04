using LockMobileClient.Models;
using LockMobileClient.Validations;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LockMobileClient.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        public ICommand RegisterCmd { get; }

        public ValidatableCode SecretCode { get; }

        Action propChangedCallBack => (RegisterCmd as Command).ChangeCanExecute;

        public RegistrationViewModel()
        {
            RegisterCmd = new Command(async () => await Register(), () => SecretCode.IsValid);
            SecretCode = new ValidatableCode(propChangedCallBack, new CodeValidator());
        }

        async Task Register()
        {

        }
    }
}
