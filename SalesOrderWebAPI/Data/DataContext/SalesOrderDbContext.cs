using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SalesOrderWebAPI.Model;

namespace SalesOrderWebAPI.Data.DataContext;

public partial class SalesOrderDbContext : DbContext
{
    public SalesOrderDbContext()
    {
    }

    public SalesOrderDbContext(DbContextOptions<SalesOrderDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblMastervariant> TblMastervariants { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductvarinat> TblProductvarinats { get; set; }

    public virtual DbSet<TblRefreshtoken> TblRefreshtokens { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblSalesHeader> TblSalesHeaders { get; set; }

    public virtual DbSet<TblSalesProductInfo> TblSalesProductInfos { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_tbl_customer");
        });

        modelBuilder.Entity<TblProductvarinat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_productvarint");
        });

        modelBuilder.Entity<TblSalesHeader>(entity =>
        {
            entity.HasKey(e => e.InvoiceNo).HasName("PK_tbl_SaleHeader");
        });

        modelBuilder.Entity<TblSalesProductInfo>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceNo, e.ProductCode }).HasName("PK_tbl_SalesInvoiceDetail");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
