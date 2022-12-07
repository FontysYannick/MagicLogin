using Magic_Interface.DTO;

namespace Magic_Interface.Interface
{
    public interface ILogin
    {
        public LoginDTO AttemptLogin(LoginDTO loginDTO);
        bool Register(LoginDTO loginDTO);

    }
}
