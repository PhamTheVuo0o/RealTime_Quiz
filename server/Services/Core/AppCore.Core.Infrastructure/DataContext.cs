using Microsoft.EntityFrameworkCore;
using AppCore.Core.Domain.Entities;
using AppCore.Infrastructure.Persistence.AppDbContext;

namespace AppCore.Core.Infrastructure
{
    public class DataContext : BaseDbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
    }
}
