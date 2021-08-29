using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            // Usado para criar as Migrações em tempo de designer da aplicação
            var connectionString = "Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=vetrigo";
            // var connectionString = "Server=.\\SQLEXPRESS;Initial Catalog=dbAPI_2;Trusted_Connection=True;MultipleActiveResultSets=true";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}
