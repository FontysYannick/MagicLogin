using Magic_Interface.Interface;
using Magic_Logic.Classes;

namespace Magic_Logic.Container
{
    public class Login_Container
    {
        private readonly ILogin _ILogin;

        public Login_Container(ILogin login)
        {
            this._ILogin = login;
        }

        public Login attemptLogin(Login login)
        {
            Login Log = new Login(_ILogin.AttemptLogin(login.GetDTO()));
            return Log;
        }

        public bool register(Login login)
        {
            return _ILogin.Register(login.GetDTO());
        }

    }
}
