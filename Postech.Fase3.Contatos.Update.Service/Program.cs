using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Postech.Fase3.Contatos.Update.Infra.Ioc;
using Postech.Fase3.Contatos.Update.Service;
using Prometheus;
using Serilog;

var builder = Host
    .CreateDefaultBuilder(args)

    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure(app =>
        {
            app.UseRouting();
            app.UseMetricServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics();
            });
        });

        webBuilder.UseUrls("http://+:5002");
    })
    .ConfigureServices((hostContext, services) =>
    {

        services.AddHostedService<WkUpdateContato>();
        services.AdicionarDependencias();
        services.AdicionarDbContext(hostContext.Configuration);
    })
    .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console());



await builder.Build().RunAsync();