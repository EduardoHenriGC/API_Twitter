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
    public class LikeController : ControllerBase
    {
        private readonly MeuContexto _context;

        public LikeController(MeuContexto context)
        {
            _context = context;
        }

        // GET: api/Like
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetLike()
        {
          if (_context.Like == null)
          {
              return NotFound();
          }
            return await _context.Like.ToListAsync();
        }

        // GET: api/Like/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> GetLike(int id)
        {
          if (_context.Like == null)
          {
              return NotFound();
          }
            var like = await _context.Like.FindAsync(id);

            if (like == null)
            {
                return NotFound();
            }

            return like;
        }

        // PUT: api/Like/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLike(int id, Like like)
        {
            if (id != like.LikeId)
            {
                return BadRequest();
            }

            _context.Entry(like).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeExists(id))
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

        // POST: api/Like
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Like>> PostLike(Like like)
        {
          if (_context.Like == null)
          {
              return Problem("Entity set 'MeuContexto.Like'  is null.");
          }
            _context.Like.Add(like);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLike", new { id = like.LikeId }, like);
        }

        // DELETE: api/Like/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            if (_context.Like == null)
            {
                return NotFound();
            }
            var like = await _context.Like.FindAsync(id);
            if (like == null)
            {
                return NotFound();
            }

            _context.Like.Remove(like);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LikeExists(int id)
        {
            return (_context.Like?.Any(e => e.LikeId == id)).GetValueOrDefault();
        }
    }
}
