using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Interfaces;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public class RegionRepository : IRegionRepository
{
    private readonly NZWalksDbContext _context;
    public RegionRepository(NZWalksDbContext context)
    {
        _context = context;
    }

    public async Task<Region> CreateAsync(Region regionModel)
    {
        await _context.Regions.AddAsync(regionModel);
        await _context.SaveChangesAsync();

        return regionModel;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    {
        var existingregionModel = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        if (existingregionModel == null) return null;

        _context.Regions.Remove(existingregionModel);
        await _context.SaveChangesAsync();

        return existingregionModel;
    }

    public async Task<List<Region>> GetAllAsync()
    {
        return await _context.Regions.ToListAsync();
    }

    public async Task<Region?> GetByIdAsync(Guid id)
    {
        var regionModel = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        if (regionModel == null) return null;

        return regionModel;
    }

    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        var existingRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        if (existingRegion == null) return null;

        existingRegion.Code = region.Code;
        existingRegion.Name = region.Name;
        existingRegion.RegionImageUrl = region.RegionImageUrl;

        await _context.SaveChangesAsync();

        return existingRegion;
    }
}
