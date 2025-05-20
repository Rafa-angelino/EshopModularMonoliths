var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config
    .ReadFrom.Configuration(context.Configuration)); // Configura o Serilog para ler as configurações do appsettings.json


// Add services to the container.

builder.Services.AddCarterWithAssemblies(
    typeof(CatalogModule).Assembly,
    typeof(BasketModule).Assembly,
    typeof(OrderingModule).Assembly);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline. 

app.MapCarter();
app.UseSerilogRequestLogging(); //adiciona middleware para logar requisições
app.UseExceptionHandler(options => { });

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.Run();
