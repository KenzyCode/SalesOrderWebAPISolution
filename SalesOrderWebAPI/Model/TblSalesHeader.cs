using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderWebAPI.Model;

[Table("tbl_SalesHeader")]
public partial class TblSalesHeader
{
    [Key]
    [StringLength(20)]
    public string InvoiceNo { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime InvoiceDate { get; set; }

    [StringLength(20)]
    public string CustomerId { get; set; } = null!;

    [Column("Customer Name")]
    [StringLength(100)]
    public string? CustomerName { get; set; }

    [Column(TypeName = "ntext")]
    public string? DeliveryAddress { get; set; }

    [Column(TypeName = "ntext")]
    public string? Remarks { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? Total { get; set; }

    [Column(TypeName = "numeric(18, 4)")]
    public decimal? Tax { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? NetTotal { get; set; }

    [StringLength(50)]
    public string? CreateUser { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? CreateDate { get; set; }

    [StringLength(50)]
    public string? ModifyUser { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? ModifyDate { get; set; }
}
