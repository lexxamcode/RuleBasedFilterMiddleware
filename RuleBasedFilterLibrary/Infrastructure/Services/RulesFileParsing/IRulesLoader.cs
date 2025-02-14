using RuleBasedFilterLibrary.Core.Model.Rules;

namespace RuleBasedFilterLibrary.Infrastructure.Services.RulesFileParsing;

public interface IRulesLoader
{
    public List<IRule> LoadRulesFromConfigurationFile(string configurationFilename);
}
