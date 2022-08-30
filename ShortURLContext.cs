using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class ShortURLContext : DbContext
{
    public ShortURLContext (DbContextOptions<ShortURLContext> options)
        : base(options)
    {
    }

    public DbSet<URLPair> URLPair { get; set; }
}
