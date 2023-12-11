using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderWebAPI.Model;

[Table("tbl_Customer")]
public partial class TblCustomer
{
    [Key]
    [StringLength(20)]
    public string Code { get; set; } = null!;

    [StringLength(200)]
    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    [StringLength(50)]
    public string? Phoneno { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? CreateUser { get; set; }

    [StringLength(50)]
    public string? ModifyUser { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifyDate { get; set; }

    public bool? IsActive { get; set; }
}
