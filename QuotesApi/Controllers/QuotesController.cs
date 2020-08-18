    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesApi.Data;
using QuotesApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuotesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotesController : ControllerBase
    {

        private QuoteDbContext _quotesDbContext;

        public QuotesController(QuoteDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_quotesDbContext.Quotes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_quotesDbContext.Quotes.Find(id));    
        }

        [HttpGet("[action]")]
        public IActionResult PageingQuotes(int pagenumber, int itemCount)
        {
            var quotes =_quotesDbContext.Quotes;
            return itemCount <= 0 || pagenumber <= 0 ? Ok(quotes) : Ok(quotes.Skip((pagenumber - 1) * itemCount).Take(itemCount));
        }
        [HttpPost]
        public IActionResult Post([FromBody]Quote quote)
        {
             _quotesDbContext.Quotes.Add(quote);
            _quotesDbContext.SaveChanges();
            return Ok("The item has been Created! ");
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            var item = _quotesDbContext.Quotes.Find(id);
            if(item == null)
            {
                return Ok("Item not found");
            }
            else
            {
                item.Name = quote.Name;
                 _quotesDbContext.SaveChanges();
                return Ok("The item updated succefully!");
            }
         
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _quotesDbContext.Quotes.Find(id);
            if(item == null)
            {
                return Ok("Item not found");
            }
            else
            {
                _quotesDbContext.Quotes.Remove(item);
                _quotesDbContext.SaveChanges();
                return Ok("Item Deleted");
            }
           
        }
    }
}
