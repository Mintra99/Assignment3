using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Models;
using Assignment3.Services.Franchises;

namespace Assignment3.Controllers
{
    [Route("api/v1/Franchise")]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService FranchiseService;

        public FranchiseController(IFranchiseService franchiseService)
        {
            FranchiseService = franchiseService;
        }

        // GET: api/Franchise
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Franchise>>> GetFranchises()
        {
            var franchises = await FranchiseService.GetAsync();

            if (franchises == null)
            {
                return NotFound();
            }

            return franchises;
        }

        // GET: api/Franchise/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Franchise>> GetFranchise(int id)
        //{
        //  if (FranchiseService.Franchises == null)
        //  {
        //      return NotFound();
        //  }
        //    var franchise = await FranchiseService.Franchises.FindAsync(id);

        //    if (franchise == null)
        //    {
        //        return NotFound();
        //    }

        //    return franchise;
        //}

        //// PUT: api/Franchise/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutFranchise(int id, Franchise franchise)
        //{
        //    if (id != franchise.Id)
        //    {
        //        return BadRequest();
        //    }

        //    FranchiseService.Entry(franchise).State = EntityState.Modified;

        //    try
        //    {
        //        await FranchiseService.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FranchiseExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Franchise
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Franchise>> PostFranchise(Franchise franchise)
        //{
        //  if (FranchiseService.Franchises == null)
        //  {
        //      return Problem("Entity set 'MovieDbContext.Franchises'  is null.");
        //  }
        //    FranchiseService.Franchises.Add(franchise);
        //    await FranchiseService.SaveChangesAsync();

        //    return CreatedAtAction("GetFranchise", new { id = franchise.Id }, franchise);
        //}

        //// DELETE: api/Franchise/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteFranchise(int id)
        //{
        //    if (FranchiseService.Franchises == null)
        //    {
        //        return NotFound();
        //    }
        //    var franchise = await FranchiseService.Franchises.FindAsync(id);
        //    if (franchise == null)
        //    {
        //        return NotFound();
        //    }

        //    FranchiseService.Franchises.Remove(franchise);
        //    await FranchiseService.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool FranchiseExists(int id)
        //{
        //    return (FranchiseService.Franchises?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
