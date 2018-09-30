namespace Product.Api.Common.Infrastructure.Persistence
{
    using System;
    using System.Text;

    public class Util
    {
        public static string getTableName(string value)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in value)
            {
                if (Char.IsUpper(c) && builder.Length > 0)
                {
                    builder.Append('_');
                }
                builder.Append(c);
            }
            return builder.ToString().ToLower();
        }
    }
}
