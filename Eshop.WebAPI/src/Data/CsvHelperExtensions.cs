using CsvHelper.Configuration;

public static class CsvHelperExtensions
{
    public static void MapSnakeCase<T>(this ClassMap<T> map)
    {
        var type = typeof(T);
        foreach (var property in type.GetProperties())
        {
            var snakeCaseName = ToSnakeCase(property.Name);
            map.Map(typeof(T), property).Name(snakeCaseName);
        }
    }

    private static string ToSnakeCase(string str)
    {
        if (string.IsNullOrEmpty(str)) return str;
        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            if (char.IsUpper(str[i]))
            {
                if (i > 0) sb.Append('_');
                sb.Append(char.ToLower(str[i]));
            }
            else
            {
                sb.Append(str[i]);
            }
        }
        return sb.ToString();
    }
}
