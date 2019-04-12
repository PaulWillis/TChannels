using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreCodeTChannel.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoreCodeTChannel
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<TChannelContext>();
      services.AddScoped<ITChannelRepository, TChannelRepository>();
      services.AddAutoMapper();

      services.AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      
      app.UseMvc();
    }
  }
}
