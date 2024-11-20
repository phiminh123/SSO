﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace NewProject.Models
{
    [Table("tbNhom")]
    public partial class tbNhom
    {

        [Key]
        public int IdNhom { get; set; }
        [StringLength(500)]
        public string MoTa { get; set; }
        [StringLength(500)]
        public string TenNhom { get; set; }
        public string DataKey { get; set; }
        public int? LinkedDataKey { get; set; }
        public int? IdParent { get; set; }
        public int? CoDataKey { get; set; }
    }
}
