using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using IpCountry.API.HostedServices.IPAddresses;
using IpCountry.Application.Countries.CommandSide.Commands.Find;
using IpCountry.Application.Countries.CommandSide.Repositories;
using IpCountry.Application.Countries.CommandSide.Services;
using IpCountry.Application.IPAdresses.CommandSide.Repositories;
using IpCountry.Domain.Notifications;
using IpCountry.Infrastructure.Core.HostedServices;
using IpCountry.Infrastructure.Core.Mediatr;
using IpCountry.Infrastructure.Core.Repositories;
using IpCountry.Infrastructure.Core.Web.API.Validators;
using IpCountry.Repository.Context;
using IpCountry.Repository.Countries;
using IpCountry.Repository.Countries.Services;
using IpCountry.Repository.IPAddresses;
using IpCountry.Repository.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IpCountryDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<HostedServiceSettings>(builder.Configuration.GetSection("HostedServiceSettings"));


builder.Services
    .AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<FindCountryCommandValidator>());

builder.Services
    .AddMemoryCache()
    .AddMediatR(typeof(FindCountryCommand).Assembly)
    .AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>()
    .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddTransient<ICountryRepository, CountryRepository>()
    .AddTransient<IIPAddressRepository, IPAddressRepository>()
    .AddTransient<ICountryService, CountryService>()
    .AddHostedService<UpdateIPAddressHostedService>()
    .AddTransient<RequestStateValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IpCountry.API", Version = "v1" });
});

var app = builder.Build();

// This part is to run migrations on build Project
//using (var serviceScope = app.Services.CreateScope())
//{
//    var dbContext = serviceScope.ServiceProvider.GetRequiredService<IpCountryDbContext>();
//    dbContext.Database.Migrate();
//} 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
