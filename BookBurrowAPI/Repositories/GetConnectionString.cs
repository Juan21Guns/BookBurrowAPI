using BookBurrowAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookBurrowAPI.Repositories
{
    public class GetConnectionString : IGetConnectionString
    {
        private readonly IConfiguration _config;

        public GetConnectionString(IConfiguration config)
        {
            _config = config;
        }

        public string nameOfString()
        {
            string? endpoint = _config.GetValue<string>("ConnectionString");
            string? username = _config.GetValue<string>("AWSUserName");
            string? password = _config.GetValue<string>("UserPassword");
            string? port = _config.GetValue<string>("DBPort");
            string v = $"Server={endpoint},{port},Database=msql-bookdb;user={username};password={password};";
            return v;
        }
    }
}
