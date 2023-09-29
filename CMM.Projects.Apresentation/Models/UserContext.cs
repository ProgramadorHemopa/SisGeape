using System.Data.Entity;

namespace CMM.Projects.Apresentation.Models
{
    public class UserContext : DbContext
    {
        public UserContext()
             : base("name=ModeloDados")
        { }
        public virtual DbSet<SystemUserModelView> systemuser { get; set; }
        public virtual DbSet<SystemUserRolesModelView> systemuserxroles { get; set; }
        public virtual DbSet<RoleModelView> roles { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}