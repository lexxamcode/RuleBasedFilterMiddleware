namespace YmlRulesFileParser.Model.Rules.Base.ParameterComparison;

using System;
using System.Collections.Generic;

public static class TypeResolver
{
    private static readonly Dictionary<string, Type> TypeMappings = new Dictionary<string, Type>
    {
        { "int", typeof(int) },
        { "integer", typeof(int) },
        { "string", typeof(string) },
        { "double", typeof(double) },
        { "float", typeof(float) },
        { "bool", typeof(bool) },
        { "boolean", typeof(bool) },
        { "char", typeof(char) },
        { "byte", typeof(byte) },
        { "short", typeof(short) },
        { "long", typeof(long) },
        { "decimal", typeof(decimal) }
    };

    public static Type GetTypeFromString(string typeName)
    {
        var type = Type.GetType(typeName);

        if (type != null)
            return type;

        if (TypeMappings.TryGetValue(typeName.ToLower(), out type))
            return type;

        throw new ArgumentException($"Unsupported type: {typeName}");
    }
}