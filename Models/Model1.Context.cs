﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projects.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class cakeEntities : DbContext
    {
        public cakeEntities()
            : base("name=cakeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<address> address { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<cart> cart { get; set; }
        public virtual DbSet<classify> classify { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<size> size { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<CartView> CartView { get; set; }
        public virtual DbSet<View_address> View_address { get; set; }
        public virtual DbSet<View_hot> View_hot { get; set; }
        public virtual DbSet<View_orders> View_orders { get; set; }
        public virtual DbSet<View_orderss> View_orderss { get; set; }
        public virtual DbSet<View_Pro> View_Pro { get; set; }
    }
}