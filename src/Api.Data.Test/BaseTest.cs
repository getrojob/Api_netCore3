using System;
using System.IO;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {
        }
    }
    public class DbTeste : IDisposable
    {
        private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}.db";
        public ServiceProvider ServiceProvider { get; private set; }
        public string DbPath { get; private set; }


        public DbTeste()
        {
            // var folder = Environment.SpecialFolder.LocalApplicationData;
            // var path = Environment.GetFolderPath(folder);
            var path = "src\\Api.Data\\baseTest";
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}" + dataBaseName;

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o =>
            // o.UseMySql($"Server=localhost;Port=3306;Database={dataBaseName};Uid=root;Pwd=vetrigo"),
            // o.UseMySql($"Server=localhost;Port=3306;Database={dataBaseName};Uid=root;Pwd=vetrigo"),
            o.UseSqlite($"Data Source={DbPath}"),
            ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
