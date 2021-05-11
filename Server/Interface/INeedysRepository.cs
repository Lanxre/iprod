using Server.Entity;

namespace Server.Interface
{
    public interface INeedysRepository
    {
        public void Add(Needys needy);
        public bool ExistNeedy(Needys needy);
    }
}