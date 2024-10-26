namespace YmlRulesFileParser.Model.Requests;

public class GetListRequestFilter
{
    public int From { get; set; }
    public int Size { get; set; } = int.MaxValue;
    public int Take { get; set; }
}