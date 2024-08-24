using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.Dtos.Difficulty;
using NZWalks.Dtos.Region;

namespace NZWalks.Dtos.Walk;

public class WalkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double LenghtInKm { get; set; }
    public string? WalkImageUrl { get; set; }

    public RegionDto Region { get; set; }
    public DifficultyDto Difficulty { get; set; }
}
