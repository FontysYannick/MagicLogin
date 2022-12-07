namespace Magic_Interface.DTO
{
    public class LoginDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public LoginDTO()
        { }

        public LoginDTO(int ID, string email, string name, string password)
        {
            this.Id = ID;
            this.Email = email;
            this.Name = name;
            this.Password = password;
        }
    }
}
