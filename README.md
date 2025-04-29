# Rule-Based Filter Middleware

Rule-Based Filter Middleware is an ASP.NET Core middleware library that provides configurable request filtering and validation through rule-based policies. 
It enables you to protect your web APIs from abuse, scraping, and various attack patterns by defining rules in YAML configuration files.

## Features

- **Rule-based request filtering**: Configure rules to allow or deny requests based on endpoints, HTTP methods, source IPs, and parameter values
- **Sequence analysis**: Detect patterns in request sequences to identify systematic data scraping or abuse
- **Configurable via YAML**: Easy configuration through human-readable YAML files
- **Request storage & history analysis**: Store and analyze request history to detect suspicious patterns
- **Extensible architecture**: Create custom sequence analyzers to detect specific patterns

## Installation

Add the Rule-Based Filter Middleware to your ASP.NET Core project using NuGet:

```
dotnet add package RuleBasedFilterMiddleware
```

## Getting Started

1. **Add the Rule-Based Filter services to your application**

```cs
// In Program.cs or Startup.cs  
var options = new RuleBasedRequestFilterOptions  
{  
    EnableRequestSequenceValidation = true,  
    ConfigurationFileName = "rulesConf.yml" // Default configuration file name  
};  
  
builder.Services  
    .AddRuleBasedRequestFilterServices(options)  
    .UseRequestStorage();
```

2. **Add the middleware to your request pipeline**

```cs
// In Program.cs or Startup.cs Configure method  
app.UseRuleBasedFilter();
```

3. **Create a rules configuration file**

Create a `rulesConf.yml` file in the root directory of your project with your rules. Here's an example:

```yml
rules:  
  - name: bulk-download-defence  
    method: get  
    endpoint: "/api/data"  
    access-policy: allow  
    argument-rules:  
      - name: z-restrict  
        type: default  
        argument-name: z  
        argument-type: int  
        argument-should-be: "> 0"  
    sequence-analyses:  
      - analysis-type: NonRandomSequenceAnalyzer  
        by-arguments:  
        - name: x  
          type: int  
        - name: y  
          type: int
```

## Creating Custom Sequence Analyzers

You can extend the functionality by creating custom sequence analyzers:

1. **Create a class that implements IRequestSequenceAnalyzer**

```cs
public class CustomSequenceAnalyzer : IRequestSequenceAnalyzer  
{  
    private readonly IRequestStorage _requestStorage;  
    private readonly RuleBasedRequestFilterOptions _options;  
  
    public CustomSequenceAnalyzer(IRequestStorage requestStorage, RuleBasedRequestFilterOptions options)  
    {  
        _requestStorage = requestStorage;  
        _options = options;  
    }  
  
    public async Task<bool> DidAnalysisSucceed(string userIp, List<ParameterSequenceAnalysis> parameterRules)  
    {  
        // Implement your custom analysis logic here  
        // Return true if the sequence is suspicious, false otherwise  
    }  
}
```

2. **Register your custom analyzer**

```cs
builder.Services.AddSequenceAnalyzer<CustomSequenceAnalyzer>();
```

3. **Reference your analyzer in the rules configuration**

```yml
sequence-analyses:  
  - analysis-type: CustomSequenceAnalyzer  
    by-arguments:  
    - name: parameter1  
      type: int  
    - name: parameter2  
      type: string
```

## Rule Configuration

Rules are defined in YAML format and support the following structure:

- **name**: Unique identifier for the rule
- **method**: HTTP method (get, post, etc.)
- **endpoint**: URL path to match
- **access-policy**: Either "allow" or "deny"
- **source-ip** (optional): Source IP to match
- **argument-rules** (optional): List of parameter validation rules
- **sequence-analyses** (optional): List of sequence analysis configurations

### Argument Rules

Each argument rule contains:

- **name**: Identifier for the rule
- **type**: Rule type (default, longitude, latitude, etc.)
- **argument-name**: Name of the parameter to validate
- **argument-type**: Data type (int, double, string, etc.)
- **argument-should-be**: Condition expression (e.g., "> 0", "== 0", "between -10 54")

### Sequence Analyses

Each sequence analysis configuration contains:

- **analysis-type**: Name of the sequence analyzer class
- **by-arguments**: List of parameters to track in the sequence

## Sample Applications

The repository includes sample applications demonstrating how to use the middleware:

- **TestTileApi**: Shows how to implement the middleware in a tile server API
- **TestWebApplication**: Demonstrates basic rule configuration and usage

## License

This project is licensed under the MIT License.

## Notes

The Rule-Based Filter Middleware is designed to protect APIs against various types of abuse patterns, including systematic data scraping, parameter manipulation, and brute force attacks. 
The sequence analysis feature is particularly useful for detecting patterns that aren't evident in individual requests but become apparent when analyzing request sequences.
