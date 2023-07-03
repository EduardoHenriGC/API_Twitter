using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_TESTE.Models;
using API_TESTE.Models.Context;

namespace API_TESTE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly MeuContexto _context;

        public SearchController(MeuContexto context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Tweet>> GetSearch(string q)
        {
            try
            {
                var searchTerm = q;
                var searchResults = _context.Tweet
                    .Where(t => t.TweetText.Contains(q) ||
                            t.User.Name.Contains(q) ||
                            t.User.UserId.Contains(q)).Select(tweet => new Tweet
                            {
                                TweetId = tweet.TweetId,
                                TweetText = tweet.TweetText,
                                CreateTime = tweet.CreateTime,
                                Likes = tweet.Likes,
                                User = new User
                                {
                                    UserId = tweet.User.UserId,
                                    Name = tweet.User.Name,
                                    Image = tweet.User.Image
                                }
                            })
                .ToList();

                return searchResults;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing SQL query: " + ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
