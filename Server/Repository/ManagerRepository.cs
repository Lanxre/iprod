using Server.Entity;

namespace Server.Repository
{
    public class ManagerRepository
    {
        public UsersRepository Users { get; }

        public ServiceRepository Service { get; }
        
        public EmploeesRepository Emploees { get; }
        
        public NeedysRepository Needys { get; }
        
        public HelpRepository Help { get; }
        
        public RoleRepository Roles { get; }

        public ManagerRepository(UsersRepository usersRepository, ServiceRepository serviceRepository, 
            EmploeesRepository emploees, NeedysRepository needys, HelpRepository help, RoleRepository roles)
        {
            Users = usersRepository;
            Service = serviceRepository;
            Emploees = emploees;
            Needys = needys;
            Help = help;
            Roles = roles;
        }

       

    }
}