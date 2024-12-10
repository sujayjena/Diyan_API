using Diyan.Application.Helpers;
using Diyan.Application.Interfaces;
using Diyan.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IJwtUtilsRepository, JwtUtilsRepository>();
            services.AddScoped<IFileManager, FileManager>();

            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<ICompanyTypeRepository, CompanyTypeRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<ITerritoryRepository, TerritoryRepository>();
            services.AddScoped<IAdminMasterRepository, AdminMasterRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IManageTrackingRepository, ManageTrackingRepository>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<IEmailConfigRepository, EmailConfigRepository>();
            services.AddScoped<IConfigRefRepository, ConfigRefRepository>();
            services.AddScoped<IManageMISRepository, ManageMISRepository>();
        }
    }
}
