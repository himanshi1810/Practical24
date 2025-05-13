using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace Employee.DAL.Services
{
    public sealed class DbContext
    {
        private readonly string _connectionString;

        public DbContext()
        {
            _connectionString = "Server=SF-CPU-0226\\SQLEXPRESS;Database=Practical_22;Trusted_Connection=True;Encrypt=False;";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
