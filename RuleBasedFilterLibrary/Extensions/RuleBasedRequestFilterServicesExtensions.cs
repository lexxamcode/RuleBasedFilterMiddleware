using Microsoft.Extensions.DependencyInjection;
using RuleBasedFilterLibrary.Core.Services.ParameterRuleFactory;
using RuleBasedFilterLibrary.Core.Services.ParameterSequenceAnalysisFactory;
using RuleBasedFilterLibrary.Core.Services.RequestSequenceValidation;
using RuleBasedFilterLibrary.Core.Services.RequestStorageManager;
using RuleBasedFilterLibrary.Core.Services.RuleFactory;
using RuleBasedFilterLibrary.Core.Services.SequenceAnalysisFactory;
using RuleBasedFilterLibrary.Infrastructure.Services.RequestStorage;
using RuleBasedFilterLibrary.Infrastructure.Services.RulesFileParsing;
using RuleBasedFilterLibrary.Middleware.Services.RequestValidation;

namespace RuleBasedFilterLibrary.Extensions;

public static class RuleBasedRequestFilterServicesExtensions
{
    public static IServiceCollection AddRuleBasedRequestFilterServices(this IServiceCollection services, RuleBasedRequestFilterOptions options)
    {
        services.AddSingleton(options);
        services.AddSingleton<IParamterSequenceAnalysisFactory, ParameterSequenceAnalysisFactory>();
        services.AddSingleton<IParameterRuleFactory, ParameterRuleFactory>();
        services.AddSingleton<IRuleFactory, RuleFactory>();
        services.AddSingleton<IRulesLoader, RulesLoader>();
        services.AddSingleton<ISequenceAnalysisFactory, SequenceAnalysisFactory>();
        services.AddSingleton<IRequestValidationService, RequestValidationService>();

        return services;
    }

    public static IServiceCollection AddSequenceAnalyzer<TAnalyzer>(this IServiceCollection services) where TAnalyzer : class, IRequestSequenceAnalyzer
    {
        services.AddSingleton<IRequestSequenceAnalyzer, TAnalyzer>();
        
        return services;
    }

    public static IServiceCollection UseRequestStorage(this IServiceCollection services, IRequestStorage? customRequestStorage = null)
    {   
        if (customRequestStorage == null)
            services.AddSingleton<IRequestStorage, OpensearchRequestStorage>();
        else
            services.AddSingleton(customRequestStorage);

        services.AddSingleton<IRequestStorageManager, RequestStorageManager>();

        return services;
    }
}
