using Server.Entity;

namespace Server.Interface
{
    public interface IUserRepository
    {
        public bool SqlMailChek(string mail);

        public void Add(Users user);

        public string ExistUser(Users user);

        public string GetAll();

    }
}