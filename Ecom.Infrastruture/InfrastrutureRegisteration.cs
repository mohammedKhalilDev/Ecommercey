using Ecom.Core.Interfaces;
using Ecom.Infrastruture.Data;
using Ecom.Infrastruture.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastruture
{
    public static class InfrastrutureRegisteration
    {
        public static IServiceCollection InfrastrutureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("EcomDB"));
            });

            return services;
        }
    }
}
