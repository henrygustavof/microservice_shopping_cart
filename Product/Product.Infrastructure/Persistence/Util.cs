namespace Product.Infrastructure.Persistence
{
    using System.Text;

    public class Util
    {
        public static string GetTableName(string value)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in value)
            {
                if (char.IsUpper(c) && builder.Length > 0)
                {
                    builder.Append('_');
                }
                builder.Append(c);
            }
            return builder.ToString().ToLower();
        }
    }
}
