using Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.DatabaseStore
{
    internal class DbContext : IDbContext
    {
        private readonly string _connectString = string.Empty;

        public DbContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            var config = builder.Build();
            this._connectString = config.GetConnectionString("Default") ?? "";
        }

        public MySqlConnection Create()
        {
            return new MySqlConnection(this._connectString);
        }

    }
}
