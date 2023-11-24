using System;
using System.Collections.Generic;
using Diplom.Client.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Client.Database.Context;

public partial class ZashitaInformationContext : DbContext
{
    public ZashitaInformationContext()
    {
    }

    public ZashitaInformationContext(DbContextOptions<ZashitaInformationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ZashitaInformation;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
