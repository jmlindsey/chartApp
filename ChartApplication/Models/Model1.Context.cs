﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChartApplication.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ChartEntities : DbContext
    {
        public ChartEntities()
            : base("name=ChartEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Intervention> Interventions { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<Vital> Vitals { get; set; }
    }
}
