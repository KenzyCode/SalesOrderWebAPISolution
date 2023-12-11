using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderWebAPI.Model;

[Table("tbl_Category")]
public partial class TblCategory
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
}
