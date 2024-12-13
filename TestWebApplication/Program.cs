using OpenSearch.Client;
using RuleBasedFilterLibrary.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRuleBasedRequestFilterServices();

var nodeAddress = new Uri("http://localhost:9200");
var config = new ConnectionSettings(nodeAddress).DefaultIndex("requests");
var client = new OpenSearchClient(config);

var deleteRequest = new DeleteIndexRequest(Indices.Parse("requests"));
await client.Indices.DeleteAsync(deleteRequest);

builder.Services.AddSingleton<IOpenSearchClient>(client);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRuleBasedFilter(new() { EnableRequestSequenceValidation = true });
app.UseAuthorization();

app.MapControllers();

app.Run();
