using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Authorization;
using ProductManagement.Data;
using ProductManagement.Filters;
using ProductManagement.Middlewares;
using ProductManagement.Services;
using System.Text;

namespace ProductManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //لما اديه اسم الفايل دايركت من غير باث معنى كدا ان الفايل موجود فى نفس الفولدر بتاع الابلكيشن وعشان يكون موجود فى نفس الفولد بعمله copy always or copy if newer
            builder.Configuration.AddJsonFile("Config.json");

            builder.Services.AddLogging(cfg => { cfg.AddDebug(); });
            var attachmentoptions = builder.Configuration.GetSection("attachments").Get<AttachmentOptions>();
            builder.Services.AddSingleton(attachmentoptions);

            builder.Services.Configure<AttachmentOptions>(builder.Configuration.GetSection("Attachments"));
            
            // Add services to the container.
            builder.Services.AddControllers(options => 
            {
                options.Filters.Add<ExecutionTimeFilter>();
                options.Filters.Add<PermissionBasedAuthorizationFilter>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
            builder.Services.AddSingleton(jwtOptions);

            builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options => 
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                };              
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<LimitingMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
