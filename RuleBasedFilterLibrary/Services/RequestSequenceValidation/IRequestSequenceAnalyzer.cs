using OpenSearch.Client;
using RuleBasedFilterLibrary.Model.Requests;
using RuleBasedFilterLibrary.Model.Rules.Base;

namespace RuleBasedFilterLibrary.Services.DeepAnalysis;

public interface IRequestSequenceAnalyzer
{
    public Task<bool> Validate(Request request, RequestParameterRule requestParameterRule);
    public Task<IndexResponse> IndexRequestAsync(Request request);
}
