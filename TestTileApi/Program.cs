using RuleBasedFilterLibrary.Core.Services.RequestSequenceAnalysis;
using RuleBasedFilterLibrary.Extensions;
using TestTileApi.CustomSequenceAnalyzers;
using TestTileApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var options = new RuleBasedRequestFilterOptions
{
    EnableRequestSequenceValidation = true,
    OnViolationAction = OnViolationHandler.OnViolationAction
};

builder.Services
    .AddRuleBasedRequestFilterServices(options)
    .UseRequestStorage()
    .AddSequenceAnalyzer<MonotonicityAnalyzer>()
    .AddSequenceAnalyzer<NonMonotonicityAnalyzer>()
    .AddSequenceAnalyzer<NonRandomSequenceAnalyzer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRuleBasedFilter();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
