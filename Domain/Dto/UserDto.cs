namespace Domain.Dto
{
    public class UserDto
    {
        public string Login { get; private set; }
        public string Password { get; private set; }

        public UserDto(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
