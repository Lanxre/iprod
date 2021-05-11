using Server.Entity;

namespace Server.Interface
{
    public interface IHelpRepository
    {
        
        public void Add(Help help);
        public string GetHelpNeedy(string data);

        public string GetHelpEmployee(string data);

        public string GetAll();

        public void Update(Help help, string data);

    }
}