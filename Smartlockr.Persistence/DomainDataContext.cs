using Microsoft.EntityFrameworkCore;

namespace Smartlockr.Persistence
{
    public class DomainDataContext : DbContext
    {

        private static bool _created = false;
        public DomainDataContext(DbContextOptions options) : base(options)
        {
            if (_created == false)
            {
                _created = true;
                Database.EnsureCreated();
            }

        }
        public DbSet<NTA7516InfoEntity> NTA7516InfoCollection { get; set; }
    }
}
