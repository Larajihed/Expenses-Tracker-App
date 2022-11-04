using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stouchi.Context;
using stouchi.Models;

namespace stouchi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BucketsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize]
        [HttpGet]
        [Route("get-balance")]
        public float GetBalance()
        {

            var Values = _context.Buckets.Where(b => b.Value > 0).Select(b => b.Value).Sum();
            return Values;
        }
        // GET: api/Buckets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bucket>>> GetBuckets()
        {
          if (_context.Buckets == null)
          {
              return NotFound();
          }
            return await _context.Buckets.ToListAsync();
        }

        // GET: api/Buckets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bucket>> GetBucket(int id)
        {
          if (_context.Buckets == null)
          {
              return NotFound();
          }
            var bucket = await _context.Buckets.FindAsync(id);

            if (bucket == null)
            {
                return NotFound();
            }

            return bucket;
        }

        // PUT: api/Buckets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBucket(int id, Bucket bucket)
        {
            if (id != bucket.BucketId)
            {
                return BadRequest();
            }

            _context.Entry(bucket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BucketExists(id))
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

        // POST: api/Buckets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bucket>> PostBucket(Bucket bucket)
        {
          if (_context.Buckets == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Buckets'  is null.");
          }
            _context.Buckets.Add(bucket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBucket", new { id = bucket.BucketId }, bucket);
        }

        // DELETE: api/Buckets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBucket(int id)
        {
            if (_context.Buckets == null)
            {
                return NotFound();
            }
            var bucket = await _context.Buckets.FindAsync(id);
            if (bucket == null)
            {
                return NotFound();
            }

            _context.Buckets.Remove(bucket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BucketExists(int id)
        {
            return (_context.Buckets?.Any(e => e.BucketId == id)).GetValueOrDefault();
        }
    }
}
