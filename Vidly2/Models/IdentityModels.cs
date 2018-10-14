using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Vidly2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    /*Identity DbContext is part of asp.net Identity framework
     When add-migration executed entity framework looked at our DbContext
     and it discovered DbSets in our DbContext whiich reference classes like user role and so on.
     and migration class calls to create these tables for asp.net identity.*/
     
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }  // Customer table in our database. 
        public DbSet<Movie> Movies { get; set; } // Movie table in our database.
        public DbSet<Genre> Genres { get; set; } // Genres table in our database
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

/*add-migration 'xxx' : xxx identifies the kind of change we have made to our domain model.
 ----> WHEN WE MAKE CHANGES WE SHOULD NOT UPDATE OUR MIGRATION IN ONE GO(TEK SEFERDE) WE SHOULD MAKE SMALL CHANGES
 CREATE A MIGRATION AND RUN IT ON A DATABASE, IF WE MAKME THESE CHANGES IN ONE GO WE INCREASE THE RISK OF THINGS
 GOING WRONG.  */