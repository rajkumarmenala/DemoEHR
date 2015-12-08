using System;


namespace Monad.EHR.Common.Utility
{
    public class AppSettings
    {
        public string SiteTitle { get; set; }
        public string GeneratedCodePath { get; set; }
		public string ImagePath { get; set; }
    }

    public static class Util
    {
        public static string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        public static string ToCamelCase(this string theString)
        {
            if (string.IsNullOrWhiteSpace(theString)) return theString;
            if (theString.Length < 2) return theString.ToLower();
            string[] words = theString.Split(new char[] { },StringSplitOptions.RemoveEmptyEntries);

            string result = "";
            foreach (string word in words)
            {
                result += word.Substring(0, 1).ToLower() + word.Substring(1);
            }
            return result;
        }
    }
}
