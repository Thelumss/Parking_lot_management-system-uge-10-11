
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Parking_lot_management_system_uge_10_11.Data;
using Parking_lot_management_system_uge_10_11.Interface;
using Parking_lot_management_system_uge_10_11.Models;
using Parking_lot_management_system_uge_10_11.Repository;

namespace Parking_lot_management_system_uge_10_11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // interface and repositry that are used
            builder.Services.AddScoped<IUserTypesRepository, UserTypesRepository>();
            builder.Services.AddScoped<ILotTypesRepository, LotTypesRepository>();
            builder.Services.AddScoped<IOrganisationRepository, OrganisationRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IParking_Lot_structursRepository, Parking_Lot_structursRepository>();
            builder.Services.AddScoped<ILotRepository, LotRepository>();
            builder.Services.AddScoped<ILot_HistoryRepostiory, Lot_HistoryRepostiory>();

            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,

                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                        },

                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("MyPolicy", opt =>
                {
                    opt.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials()
                       .WithOrigins("http://localhost:4200");
                });
            });


            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer("Data Source=SPAC-PF5GM5KK\\SQLEXPRESS;Initial Catalog=ParkingSystem;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            });


            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            SeedData.Initialize(services);

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
        }
    }
}
