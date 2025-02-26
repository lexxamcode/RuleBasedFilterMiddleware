namespace RuleBasedFilterLibrary.Core.Model.Comparison;

public interface IComparer
{
    public int Compare(object? actualValue, object? ethalonValue);
}
