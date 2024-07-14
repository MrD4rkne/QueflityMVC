using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public class Image : BaseEntity
{
    public string FileUrl { get; set; }

    public string AltDescription { get; set; }
}