using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderWebAPI.Model;

[Table("tbl_Mastervariant")]
public partial class TblMastervariant
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string VarintName { get; set; } = null!;

    public string? VarinatType { get; set; }

    public bool? IsActive { get; set; }
}
