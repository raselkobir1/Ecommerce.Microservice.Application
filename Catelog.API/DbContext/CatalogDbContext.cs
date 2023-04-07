using MongoRepo.Context;

namespace Catelog.API.DbContext
{
    public class CatalogDbContext : ApplicationDbContext
    {
        static IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        static string conString = config.GetConnectionString("Catalog.API");
        static string dataBase = config.GetValue<string>("DatabaseName");
        public CatalogDbContext() : base(conString, dataBase)
        {
        }
    }
}
