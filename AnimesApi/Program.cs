
using Animes.Application.Handlers;
using Animes.Domain.Interface;
using Animes.Infra.Persistence;
using Animes.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<AnimeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();

        //Config MediatR
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(SearchAnimeHandler).Assembly)
        );

        builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .ReadFrom.Configuration(ctx.Configuration)
);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "AnimesApi",
                Version = "3.0.0",
                Description = "Api para Gerenciar Animes"
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Startup");
    var db = scope.ServiceProvider.GetRequiredService<AnimeDbContext>();

    var attempts = 0;
    var maxAttempts = 10;
    var delay = TimeSpan.FromSeconds(3);

    while (true)
    {
        try
        {
            logger.LogInformation("Aplicando migrations...");
            db.Database.Migrate();
            logger.LogInformation("Migrations aplicadas com sucesso.");
            break;
        }
        catch (Exception ex)
        {
            attempts++;
            logger.LogWarning(ex, "Falha ao aplicar migrations (tentativa {Attempt}/{Max}).", attempts, maxAttempts);
            if (attempts >= maxAttempts) throw;
            Thread.Sleep(delay); // ✅ espera síncrona entre tentativas
        }
    }
}


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnimesApi v1");
                c.RoutePrefix = "swagger";
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}