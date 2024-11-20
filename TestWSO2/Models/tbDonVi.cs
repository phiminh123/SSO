using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NewProject.Models
{
    public partial class tbDonVi
    {
        [Key]
        public int IdDonVi { get; set; }
        public string TenDonVi { get; set; }
    }
}

