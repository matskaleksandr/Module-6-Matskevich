using System.Data.Entity;

namespace Solution1
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}
