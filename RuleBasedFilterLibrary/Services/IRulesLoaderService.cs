using YmlRulesFileParser.Model.Rules.Base;

namespace RuleBasedFilterLibrary.Services;

public interface IRulesLoaderService
{
    public List<RequestRule> LoadRulesFromConfigurationFile(string configurationFilename);
}
