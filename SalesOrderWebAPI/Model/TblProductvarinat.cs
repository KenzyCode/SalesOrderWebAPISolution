using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderWebAPI.Model;

[Table("tbl_productvarinat")]
public partial class TblProductvarinat
{
    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string ProductCode { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string ColorCode { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string SizeCode { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? Remarks { get; set; }

    [Column(TypeName = "decimal(18, 3)")]
    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }
}
