using Microsoft.EntityFrameworkCore;

namespace TaxHistoryApi.models
{
    public class HistoryContext : DbContext
    {
        public HistoryContext(DbContextOptions<HistoryContext> options) : base(options) { }
        public DbSet<HistoryItem> HistoryItems { get; set; }
    }
}