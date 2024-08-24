using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.Dtos.Region;

public class UpdateRegionFromRequestDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Code has to be 3 characters")]
    [MaxLength(3, ErrorMessage = "Code has to be 3 characters")]
    public string Code { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Code has to be 100 characters")]
    public string Name { get; set; }
    public string? RegionImageUrl { get; set; }
}
