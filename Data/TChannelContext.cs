using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoreCodeTChannel.Data
{
  public class TChannelContext : DbContext
  {
    private readonly IConfiguration _config;

    public TChannelContext(DbContextOptions options, IConfiguration config) : base(options)
    {
            _config = config;
    }

    public DbSet<TChannel> TChannels { get; set; } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("CodeTChannel"));
    }

    protected override void OnModelCreating(ModelBuilder bldr)
    { 
    }

  }
}
