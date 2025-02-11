using RuleBasedFilterLibrary.Core.Model.Rules;

namespace RuleBasedFilterLibrary.Infrastructure.Services.RulesFileParsing;

public interface IRulesLoader
{
    public List<Rule> LoadRulesFromConfigurationFile(string configurationFilename);
}
