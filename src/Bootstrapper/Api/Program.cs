var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

var app = builder.Build();

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

// Configure the HTTP request pipeline
app.Run();