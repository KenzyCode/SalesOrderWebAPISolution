using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderWebAPI.Model;

[Table("tbl_product")]
public partial class TblProduct
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column(TypeName = "decimal(18, 3)")]
    public decimal? Price { get; set; }

    public int? Category { get; set; }
}
