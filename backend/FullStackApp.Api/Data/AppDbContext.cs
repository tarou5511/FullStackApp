using System;
using FullStackApp.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullStackApp.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
}
