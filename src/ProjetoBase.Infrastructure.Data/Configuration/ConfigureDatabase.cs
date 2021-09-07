using Microsoft.EntityFrameworkCore;

namespace ProjetoBase.Infrastructure.Data.Configuration
{
    public static class ConfigureDatabase
    {
        public static void SetConnectionString(DbContextOptionsBuilder options, string connectionString) => options.UseSqlServer(connectionString);
    }
}
