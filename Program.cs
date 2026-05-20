using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.ComponentModel;
using Licenses.Models;
using Licenses.Repositories.ClientRepositories;
using Licenses.Repositories.ActivityTypeRepositories;
using Licenses.Repositories.ExcutivePositionRepositories;
using Licenses.Repositories.StageRepositories;
using Licenses.Repositories.StepRepositories;
using Licenses.Repositories.FeesRepositories;
using Licenses.Repositories.LotRepositories;
using Licenses.Repositories.OrderRepositpories;
using Licenses.Repositories.TransactionRepositories;
using Licenses.Repositories.LotOrderRepository;
using Licenses.Repositories.StepOrderRepositories;
using Licenses.Repositories.LotOrderStepsRepositpories;
using Licenses.Repositories.TransactionStagesRepository;
using Licenses.Repositories.TransactionLotOrderStagesRepositories;

namespace Licenses
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            #region ConnectionString
            var connectionString = builder.Configuration.GetConnectionString("BasicCS");
            builder.
                Services.
                AddDbContext<LicensesContext>(options => options.
                UseSqlServer(connectionString));
            #endregion
            #region DependencyInjection
            builder.
                Services.
                AddScoped<DbContext, LicensesContext>();
            builder.
                Services.
                AddScoped<IClientRepository,ClientRepository>();
            builder.
                Services.
                AddScoped<IActivityTypeRepository,ActivityTypeRepository>();
            builder.
                Services.
                AddScoped<IExcutivePositionRepository,ExcutivePositionRepository>();
            builder.
                Services.
                AddScoped<IStepRepository,StepRepository>();
            builder.
                Services.
                AddScoped<IStageRepository,StageRepository>();
            builder.
                Services.
                AddScoped<IFeesRepository, FeesRepository>();
            builder.
                Services.
                AddScoped<ILotRepository,LotRepository>();
            builder.
                Services.
                AddScoped<IOrderRepository,OrderRepository>();
            builder.
                Services.
                AddScoped<ITransactionRepository,TransactionRepository>();
            builder.
                Services.
                AddScoped<ILotOrderRepository,LotOrderRepository>();
            builder.
                Services.
                AddScoped<IStepOrderRepository,StepOrderRepository>();

            builder.
                Services.
                AddScoped<ILotOrderStepsRepository, LotOrderStepsRepository>();
            builder.
                Services.
                AddScoped<ITransactionStagesRepository,TransactionStagesRepository>();
            builder. 
                Services.
                AddScoped<ITransactionLotOrderStagesRepository,TransactionLotOrderStagesRepository>();
            #endregion
            #region AddLocalizationForArabic 
            builder.Services.AddLocalization(op => op.ResourcesPath = "Resources");
            #endregion
            var app = builder.Build();
            #region SupportCulture
            var supportedCultures = new[]
            {
               new CultureInfo("ar"),      // Arabic
               new CultureInfo("en")       // Optional fallback
            };
            app.UseRequestLocalization(
                new RequestLocalizationOptions
                {
                    DefaultRequestCulture=new RequestCulture("ar"),
                    SupportedCultures=supportedCultures,
                    SupportedUICultures=supportedCultures

                });

            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
