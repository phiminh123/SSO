using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewProject.Models;

[Table("tbFile")]
public partial class tbFile
{
    [Key]
    [StringLength(36)]
    public string uid { get; set; } = null!;

    public string? filename { get; set; }

    [StringLength(10)]
    public string? bucket { get; set; }
}
