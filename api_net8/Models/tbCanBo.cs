using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace api.Models
{
    [Table("tbCanBo")]
    public partial class tbCanBo
    {
        [Key]
        public int IdCanBo { get; set; }
        public string HoTen { get; set; }
        public DateOnly NgaySinh { get; set; }
        public int? IdDonVi { get; set; }
    }
}

