namespace Fictoria.Runner;

public static class PrettyPrint
{
    public static string Format(object value)
    {
        switch (value)
        {
            case bool b:
                return b.ToString().ToLower();
            case string s:
                return $"\"{s}\"";
            case List<object> list:
                var formatted1 = list.Select(Format);
                var delimited1 = string.Join(", ", formatted1);
                return $"[{delimited1}]";
            case Dictionary<string, object> @struct:
                var formatted2 = @struct.Select(
                    kv => $"\"{kv.Key}\": {Format(kv.Value)}"
                );
                var delimited2 = string.Join(", ", formatted2);
                return $"{{{delimited2}}}";
            default:
                return value.ToString() ?? "MISSING";
        }
    }
}