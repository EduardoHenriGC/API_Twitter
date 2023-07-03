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
    public class TweetController : ControllerBase
    {
        private readonly MeuContexto _context;

        public TweetController(MeuContexto context)
        {
            _context = context;
        }

        // GET: api/Tweet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tweet>>> GetTweet()
        {
            var tweets = await _context.Tweet
                .Include(tweet => tweet.User)
                .Include(tweet => tweet.Likes)
                    .ThenInclude(like => like.User)
                .Include(tweet => tweet.Comments) // Inclui os comentários relacionados aos tweets
                    .ThenInclude(comment => comment.User) // Inclui o usuário associado a cada comentário
                .Select(tweet => new Tweet
                {
                    TweetId = tweet.TweetId,
                    TweetText = tweet.TweetText,
                    CreateTime = tweet.CreateTime,
                    User = new User
                    {
                        UserId = tweet.User.UserId,
                        Name = tweet.User.Name,
                        Image = tweet.User.Image
                    },
                    Likes = tweet.Likes.Select(like => new Like
                    {
                        LikeId = like.LikeId,
                        UserId = like.UserId,
                        TweetId = like.TweetId
                    }).ToList(),
                    Comments = tweet.Comments.Select(comment => new Comment
                    {
                        CommentId = comment.CommentId,
                        CommentText = comment.CommentText,
                        CreateTime = comment.CreateTime,
                        UserId = comment.UserId,
                        
                    }).ToList()
                })
                .ToListAsync();

            if (tweets.Count == 0)
            {
                return NotFound();
            }

            return tweets;
        }


        [HttpPost("{authorEmail}")]
        public ActionResult<IEnumerable<Tweet>> GetTweetsByAuthorEmail(string authorEmail)
        {
            var filteredTweets = _context.Tweet
                .Include(tweet => tweet.User)
                .Where(tweet => tweet.User.UserId == authorEmail)
                .Select(tweet => new Tweet
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

            return Ok(filteredTweets);
        }

        // GET: api/Tweet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tweet>> GetTweet(int id)
        {
          if (_context.Tweet == null)
          {
              return NotFound();
          }
            var tweet = await _context.Tweet.FindAsync(id);

            if (tweet == null)
            {
                return NotFound();
            }

            return tweet;
        }

        // PUT: api/Tweet/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTweet(int id, Tweet tweet)
        {
            if (id != tweet.TweetId)
            {
                return BadRequest();
            }

            _context.Entry(tweet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TweetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tweet
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tweet>> PostTweet(Tweet tweet)
        {
          if (_context.Tweet == null)
          {
              return Problem("Entity set 'MeuContexto.Tweet'  is null.");
          }
            _context.Tweet.Add(tweet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTweet", new { id = tweet.TweetId }, tweet);
        }

        // DELETE: api/Tweet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTweet(int id)
        {
            if (_context.Tweet == null)
            {
                return NotFound();
            }
            var tweet = await _context.Tweet.FindAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }

            _context.Tweet.Remove(tweet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TweetExists(int id)
        {
            return (_context.Tweet?.Any(e => e.TweetId == id)).GetValueOrDefault();
        }
    }
}
