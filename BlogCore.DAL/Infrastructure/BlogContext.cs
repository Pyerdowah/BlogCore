using Blog.DAL.Model;
using BlogCore.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Infrastructure
{
    public class BlogContext : DbContext
    {
        private string _connectionString;
        private string _defaultConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\_TMP\\BlogCore\\BlogCore.DAL.Tests\\Database1.mdf;Integrated Security=True";
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogContext() : base()
        {
            _connectionString = _defaultConnection;
        }

        public BlogContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
