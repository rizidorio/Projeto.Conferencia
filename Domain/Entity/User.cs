namespace Domain.Entity
{
    public class User
    {
        public int Id { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }

        private User()
        { }
        public User(string login, string password, int id = 0)
        {
            Id = id;
            Login = login;
            Password = password;
        }
    }
}
