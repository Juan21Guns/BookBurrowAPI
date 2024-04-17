using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BookBurrowAPI.Models
{
    public class UsersContext : DbContext
    {
        private readonly IConfiguration _config;
        private readonly string? connectionString;
        public UsersContext(IConfiguration config)
        {
            _config = config;
            connectionString = _config.GetConnectionString("default");
        }
        public DbSet<Users> User {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySQL(connectionString);
    }
}
