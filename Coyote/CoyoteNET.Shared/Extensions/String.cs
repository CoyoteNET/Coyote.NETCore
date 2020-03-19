using System;

namespace CoyoteNET.Shared.Extensions
{
    public static class String
    {
        public static string ReplaceAt(this string str, int index, int length, string replace)
        {
            return str.Remove(index, Math.Min(length, str.Length - index))
                    .Insert(index, replace);
        }
    }
}
