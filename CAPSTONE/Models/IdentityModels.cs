using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace CAPSTONE.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {




        public string Code { get; set; }

        public string RoleName { get; set; }

        public bool Member { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName):base(roleName) { }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<OffenseStats> Offense { get; set; }

        public DbSet<DefenseStats> Defense { get; set; }

        public DbSet<Coach> Coaches { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<LineUp> Lineups { get; set; }

        public DbSet<SubmitDefense> SubmitDefenses { get; set; }

        public DbSet<SubmitPitching> SubmitPitchings { get; set; }

        public DbSet<PitchStats> PitchStats { get; set; }

        public DbSet<SubmitOffense> SubmitOffenses { get; set; }

        public DbSet <Calendar> Calendar { get; set; }

        public DbSet<TotalOffense> GameOffenses { get; set; }

        public DbSet<TotalDefense> TotalDefenses { get; set; }

        public System.Data.Entity.DbSet<CAPSTONE.Models.TotalPitching> TotalPitchings { get; set; }

       public DbSet<Location> Location { get; set; }

        public DbSet<TeamConfirm> Code { get; set; }

        public System.Data.Entity.DbSet<CAPSTONE.Models.ContactCoach> ContactCoaches { get; set; }

        public System.Data.Entity.DbSet<CAPSTONE.Models.ContactPlayer> ContactPlayers { get; set; }
    }
}