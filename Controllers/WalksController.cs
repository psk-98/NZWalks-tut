using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Dtos.Walk;
using NZWalks.Interfaces;
using NZWalks.Models.Domain;

namespace NZWalks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _walkRepo;
    public WalksController(IMapper mapper, IWalkRepository walkRepo)
    {
        _mapper = mapper;
        _walkRepo = walkRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddWalkFromRequestDto addWalkDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var walkModel = _mapper.Map<Walk>(addWalkDto);
        await _walkRepo.CreateAsync(walkModel);
        var walkDto = _mapper.Map<WalkDto>(walkModel);

        return Ok(walkDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending
    , [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 4)
    {
        var walksModel = await _walkRepo.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
        var walkDto = _mapper.Map<List<WalkDto>>(walksModel);
        return Ok(walkDto);
    }
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var walkModel = await _walkRepo.GetByIdAsync(id);
        if (walkModel == null) return NotFound();

        var walkDto = _mapper.Map<WalkDto>(walkModel);

        return Ok(walkDto);

    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkFromRequestDto updateDto)
    {
        var walkModel = _mapper.Map<Walk>(updateDto);
        await _walkRepo.UpdateAsync(id, walkModel);

        if (walkModel == null) return NotFound();
        var walkDto = _mapper.Map<WalkDto>(walkModel);

        return Ok(walkDto);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var walkModel = await _walkRepo.DeleteAsync(id);
        if (walkModel == null) return NotFound();

        var walkDto = _mapper.Map<WalkDto>(walkModel);

        return Ok(walkDto);
    }
}
