using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Models;
using Assignment3.Services.Franchises;
using AutoMapper;
using Assignment3.Data.Dtos.Franchises;

namespace Assignment3.Controllers
{
    [Route("api/v1/Franchise")]
    [ApiController]
    [Produces("application/Json")]
    [Consumes("application/Json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        private readonly IMapper _mapper;

        public FranchiseController(IFranchiseService franchiseService, IMapper mapper)
        {
            _franchiseService = franchiseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all franchises
        /// </summary>
        /// <returns>All franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetFranchises()
        {
            var franchises = await _franchiseService.GetAsync();

            return Ok(_mapper.Map<IEnumerable<FranchiseDto>>(franchises));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDto>> GetFranchise(int id)
        {
            var franchise = await _franchiseService.GetByIdAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            
            return _mapper.Map<FranchiseDto>(franchise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchisePutDto franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }

            var franchiseToUpdate = _mapper.Map<Franchise>(franchise);
            var updatedFranchise = await _franchiseService.UpdateAsync(franchiseToUpdate);

            if (updatedFranchise == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Franchise
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FranchiseDto>> PostFranchise(FranchisePostDto franchise)
        {
            var franchiseToAdd = _mapper.Map<Franchise>(franchise);
            var addedFrannchise = await _franchiseService.CreateAsync(franchiseToAdd);


            return CreatedAtAction(
                nameof(GetFranchise),
                new { id = addedFrannchise.Id},
                _mapper.Map<FranchiseDto>(addedFrannchise));
        }

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
