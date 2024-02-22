using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public class Image : BaseEntity
{
    public required string FileUrl { get; set; }

    public required string AltDescription { get; set; }
}