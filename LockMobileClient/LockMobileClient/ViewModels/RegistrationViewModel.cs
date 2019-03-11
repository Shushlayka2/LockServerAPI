using LockMobileClient.Models;
using LockMobileClient.Services;
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
        public IRemoteServerSyncProxy RemoteServerSyncProxy { get; }
        public ValidatableCode SecretCode { get; }

        Action propChangedCallBack => (RegisterCmd as Command).ChangeCanExecute;

        public RegistrationViewModel(IRemoteServerSyncProxy proxy)
        {
            RegisterCmd = new Command(async () => await Register(), () => SecretCode.IsValid);
            RemoteServerSyncProxy = proxy;
            SecretCode = new ValidatableCode(propChangedCallBack, new CodeValidator());
        }

        async Task Register()
        {
            var deviceId = RemoteServerSyncProxy.Register(SecretCode.Value);
        }
    }
}
