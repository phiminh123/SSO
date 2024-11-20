using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace NewProject.Models
{
    [Table("tbUser")]
    [Index(nameof(UserName), Name = "UC1_UserName", IsUnique = true)]

    public partial class tbUser
    {
        [Key]
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public int? IdCanBo { get; set; }
        public int? IdNhom { get; set; }
        public bool? IsLDAPAccount { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public string token { set; get; }
        [NotMapped]
        public bool Checked { get; set; }
        [NotMapped]
        public bool IsAuthenticated { get; set; }
    }
}

