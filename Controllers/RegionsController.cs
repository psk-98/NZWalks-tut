using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.CustomActionFilter;
using NZWalks.Data;
using NZWalks.Dtos.Region;
using NZWalks.Interfaces;
using NZWalks.Models.Domain;

namespace NZWalks.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IRegionRepository _regionRepo;
    private readonly IMapper _mapper;
    public RegionsController(IMapper mapper, IRegionRepository regionRepo)
    {
        _regionRepo = regionRepo;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var regionModel = await _regionRepo.GetAllAsync();

        var regionDto = _mapper.Map<List<RegionDto>>(regionModel);


        return Ok(regionDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var regionModel = await _regionRepo.GetByIdAsync(id);

        if (regionModel == null) return NotFound();

        var regionDto = _mapper.Map<List<RegionDto>>(regionModel);

        return Ok(regionDto);
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] CreateRegionFromRequestDto createRegionDto)
    {
        // if (!ModelState.IsValid) return BadRequest(ModelState); // replaced with ValidateModel decorator

        var regionModel = _mapper.Map<Region>(createRegionDto);


        regionModel = await _regionRepo.CreateAsync(regionModel);
        var regionDto = _mapper.Map<List<RegionDto>>(regionModel);


        return CreatedAtAction(nameof(GetById), new { id = regionModel.Id }, regionDto);
    }
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionFromRequestDto updateRegionDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existingRegionModel = _mapper.Map<Region>(updateRegionDto);

        var regionModel = await _regionRepo.UpdateAsync(id, existingRegionModel);
        if (regionModel == null) return NotFound();

        var regionDto = _mapper.Map<RegionDto>(regionModel);

        return Ok(regionDto);

    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var regionModel = await _regionRepo.DeleteAsync(id);
        if (regionModel == null) return NotFound();

        var regionDto = _mapper.Map<RegionDto>(regionModel);

        return Ok(regionDto);
    }


}
