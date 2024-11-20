using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NewProject.Models
{
    public partial class tbCanBo
    {
        [Key]
        public int IdCanBo { get; set; }
        public string HoTen { get; set; }
        public DateOnly NgaySinh { get; set; }
        public int? IdDonVi { get; set; }
    }
}

