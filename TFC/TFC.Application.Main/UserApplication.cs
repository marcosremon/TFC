using TFC.Application.Interface;
namespace TFC.Application.Main
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserDomain _userDomain;

        public UserApplication(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }
    }
}