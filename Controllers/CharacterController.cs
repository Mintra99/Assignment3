﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Models;
using AutoMapper;
using Assignment3.Data.Dtos.Characters;
using Assignment3.Services.Characters;
using Assignment3.Exceptionhandler;
using Assignment3.Data.Dtos.Franchises;

namespace Assignment3.Controllers
{
    [Route("api/v1/Character")]
    [ApiController]
    [Produces("application/Json")]
    [Consumes("application/Json")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public CharacterController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharacters()
        {
            var characters = await _characterService.GetAsync();
            return Ok(_mapper.Map<IEnumerable<CharacterDto>>(characters));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDto>> GetCharacter(int id)
        {
            try
            {
                return Ok(_mapper.Map<CharacterDto>(await _characterService.GetByIdAsync(id)));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }

            try
            {
                await _characterService.UpdateAsync(_mapper.Map<Character>(character));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CharacterDto>> PostCharacter(Character character)
        {
         var newCharacter = await _characterService.CreateAsync(_mapper.Map<Character>(character));

            return CreatedAtAction("GetCharacter", 
                new { id = newCharacter.Id },
                _mapper.Map<CharacterDto>(newCharacter));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            try
            {
                await _characterService.DeleteAsync(id);
                return NoContent();
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
