using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.DAL
{
    public class DeliverContextFactory : IDesignTimeDbContextFactory<DeliverContext>
    {
        public DeliverContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeliverContext>();

            // Укажите строку подключения вручную или прочитайте её из конфигурационного файла
            optionsBuilder.UseMySql(
                "Server=localhost;Database=deliver;User=root;Password=0000;",
                new MySqlServerVersion(new Version(8, 0, 32))
            );

            return new DeliverContext(optionsBuilder.Options);
        }
    }
}
