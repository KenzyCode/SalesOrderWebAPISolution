using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderWebAPI.Model;

[PrimaryKey("InvoiceNo", "ProductCode")]
[Table("tbl_SalesProductInfo")]
public partial class TblSalesProductInfo
{
    [Key]
    [StringLength(20)]
    public string InvoiceNo { get; set; } = null!;

    [Key]
    [StringLength(20)]
    public string ProductCode { get; set; } = null!;

    [StringLength(100)]
    public string? ProductName { get; set; }

    public int? Qty { get; set; }

    [Column(TypeName = "numeric(18, 3)")]
    public decimal? SalesPrice { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? Total { get; set; }

    [StringLength(50)]
    public string? CreateUser { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? ModifyUser { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? ModifyDate { get; set; }
}
