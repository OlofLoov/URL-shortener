using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShortURLContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ShortUrlsContext")));

builder.Services.AddTransient<IURLService, URLService>();
builder.Services.AddSingleton<IKeyGenerator, KeyGenerator>();
builder.Services.AddSingleton<URLMemoryCache<URLPair>, URLMemoryCache<URLPair>>();

var app = builder.Build();
app.UseRouting();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "swagger";
});

app.UseEndpoints((endpoints) =>
{
    endpoints.MapGet("/", (httpContext) =>
    {
        return httpContext.Response.SendFileAsync("index.html");
    });
});

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
