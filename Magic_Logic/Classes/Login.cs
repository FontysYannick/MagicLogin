using Magic_Interface.DTO;

namespace Magic_Logic.Classes
{
    public class Login
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Login()
        { }

        public Login(int ID, string email, string name, string password)
        {
            this.Id = ID;
            this.Email = email;
            this.Name = name;
            this.Password = password;
        }

        public Login(LoginDTO dto)
        {
            this.Id = dto.Id;
            this.Email = dto.Email;
            this.Name = dto.Name;
            this.Password = dto.Password;
        }

        public LoginDTO GetDTO()
        {
            LoginDTO dto = new LoginDTO(Id, Email, Name, Password);
            return dto;
        }
    }
}
