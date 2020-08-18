using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using QuotesApi.Models;

namespace QuotesApi.Data
{
    public class QuoteDbContext :DbContext
    {
        public QuoteDbContext(DbContextOptions<QuoteDbContext> options): base(options)
        {
        }
        public DbSet<Quote> Quotes { get; set; }
       
    }
}
