using RuleBasedFilterLibrary.Extensions;
using TileServerApi.Model;
using TileServerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRuleBasedRequestFilterServices(RuleBasedRequestFilterOptions.Default);

var tilesDirectory = builder.Configuration["Tiles:LocalStoragePath"] ?? string.Empty;

builder.Services.AddSingleton<ITileRepository>(_ => new LocalTileRepository(tilesDirectory));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRuleBasedFilter(new() { EnableRequestSequenceValidation = true });
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
