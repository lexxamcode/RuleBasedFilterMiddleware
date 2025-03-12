namespace RuleBasedFilterLibrary.Core.Model.Comparison;

public class DefaultComparer : IComparer
{
    public int Compare(object? actualValue, object? ethalonValue)
    {
        return CastToIComparableAndCompare(actualValue, ethalonValue);
    }

    protected virtual int CastToIComparableAndCompare(object? actualValue, object? ethalonValue)
    {
        if (actualValue is not IComparable actualValueAsComparable ||
            ethalonValue is not IComparable ethalonValueAsComparable)
            throw new ArgumentException("actualValue or ethalonValue are not IComparable");

        return actualValueAsComparable.CompareTo(ethalonValueAsComparable);
    }
}
