using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Dtos.Region;
using NZWalks.Models.Domain;

namespace NZWalks.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly NZWalksDbContext _context;
    public RegionsController(NZWalksDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var regionsModel = _context.Regions.ToList();

        var regionsDto = new List<RegionDto>();
        foreach (var regionModel in regionsModel)
        {
            regionsDto.Add(new RegionDto()
            {
                Id = regionModel.Id,
                Code = regionModel.Code,
                Name = regionModel.Name,
                RegionImageUrl = regionModel.RegionImageUrl
            });
        }
        return Ok(regionsDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var regionModel = _context.Regions.FirstOrDefault(r => r.Id == id);
        if (regionModel == null) return NotFound();

        var regionDto = new RegionDto
        {
            Id = regionModel.Id,
            Code = regionModel.Code,
            Name = regionModel.Name,
            RegionImageUrl = regionModel.RegionImageUrl
        };
        return Ok(regionDto);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateRegionFromRequestDto createRegionDto)
    {
        var regionModel = new Region
        {
            Code = createRegionDto.Code,
            Name = createRegionDto.Name,
            RegionImageUrl = createRegionDto.RegionImageUrl
        };

        _context.Regions.Add(regionModel);
        _context.SaveChanges();

        var regionDto = new RegionDto
        {
            Id = regionModel.Id,
            Code = regionModel.Code,
            Name = regionModel.Name,
            RegionImageUrl = regionModel.RegionImageUrl
        };

        return CreatedAtAction(nameof(GetById), new { id = regionModel.Id }, regionDto);
    }
}
