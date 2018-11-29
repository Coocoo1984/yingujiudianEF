using System;
using Microsoft.EntityFrameworkCore;

namespace DevelopBase.Data
{
    public abstract class DbcontextBase : DbContext
    {
        private string _connectionString = "";
        protected string ConnectionString
        {
            get => _connectionString;
        }
        public DbcontextBase(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException();
            }
            _connectionString = connectionString;

        }
    }
}
