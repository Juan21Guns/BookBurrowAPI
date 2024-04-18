using BookBurrowAPI.Data;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace BookBurrowAPI.Repositories
{
    public class TestSQL : ITestSQL
    {
        private readonly DataContext _dbContext;
        public TestSQL(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void GetTest()
        {
            Console.WriteLine(_dbContext.Users
                .OrderBy(c => c.UserId)
                .First()
                .FirstName
            );
            Console.WriteLine("Hi this workssss");
        }

    }
}
