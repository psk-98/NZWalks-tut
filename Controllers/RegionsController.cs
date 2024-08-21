using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Dtos;

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
}
