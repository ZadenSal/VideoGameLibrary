using Microsoft.EntityFrameworkCore;
using VideoGameLibrary.Models;

namespace VideoGameLibrary.Data
{
    public class VideoGameDBContext : DbContext
    {
        public VideoGameDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Game> Games { get; set; }
    }
}
