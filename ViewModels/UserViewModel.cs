namespace LockServerAPI.ViewModels
{
    public class UserViewModel
    {
        public string Token { get; set; }

        public UserViewModel(string token)
        {
            Token = token;
        }
    }
}