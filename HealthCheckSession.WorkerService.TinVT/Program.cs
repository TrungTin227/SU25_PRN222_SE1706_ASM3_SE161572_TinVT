using HealthCheckSession.WorkerService.TinVT;
using Microsoft.EntityFrameworkCore;
using SMMS.Repositories.TinVT;
using SMMS.Repositories.TinVT.Models;
using SMMS.Services.TinVT;
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "TinVT_WorkerService"; //sc create "TinVT_WorkerService" BinPath="D:\SU25_PRN222_SE1706_ASM3_SE161572_TinVT\HealthCheckSession.WorkerService.TinVT\bin\Debug\net8.0\HealthCheckSession.WorkerService.TinVT.exe"

});
builder.Services.AddDbContext<SU25_PRN222_SE1706_G1_SMMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddSingleton<IServiceProviders, ServiceProviders>();
builder.Services.AddScoped<UserAccountService>();
builder.Services.AddScoped<IHealthCheckSessionTinVTService, HealthCheckSessionTinVTService>();
builder.Services.AddScoped<HealthCheckSessionTinVTRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<HealthCheckStudentTinVtService>();
builder.Services.AddScoped<HealthCheckStudentTinVtRepository>();


builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
