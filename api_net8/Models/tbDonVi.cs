using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace api.Models
{
    [Table("tbDonVi")]
    public partial class tbDonVi
    {
        [Key]
        public int IdDonVi { get; set; }
        public string TenDonVi { get; set; }
    }
}

