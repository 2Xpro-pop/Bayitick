using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bayitick.Models;
public class AppDbContext : DbContext
{

    public DbSet<Recept> Recepts
    {
        get; set;
    } = null!;

    public DbSet<Resource> Resources
    {
        get; set;
    } = null!;

    public DbSet<ResourceCountForRecept> CountForRecepts
    {
        get; set;
    }

    public AppDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=baitisimo.db");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
