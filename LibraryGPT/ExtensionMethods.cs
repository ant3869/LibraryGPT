//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using System.Net.Http;

//namespace LibraryGPT
//{
//    public static class ExtensionMethods
//    {
//        // String Extensions

//        // Check if a string is a valid email.
//        public static bool IsValidEmail(this string email)
//        {
//            var pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
//            return Regex.IsMatch(email, pattern);
//        }

//        // Capitalize the first letter of a string.
//        public static string CapitalizeFirstLetter(this string input)
//        {
//            if (string.IsNullOrEmpty(input)) return string.Empty;
//            return char.ToUpper(input[0]) + input.Substring(1);
//        }

//        // Check if a string is numeric.
//        public static bool IsNumeric(this string input)
//        {
//            return double.TryParse(input, out _);
//        }

//        // IEnumerable Extensions

//        // Check if a collection is empty or null.
//        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
//        {
//            return collection == null || !collection.Any();
//        }

//        // Get a random item from a collection.
//        public static T RandomItem<T>(this IEnumerable<T> collection)
//        {
//            var random = new Random();
//            return collection.ElementAt(random.Next(collection.Count()));
//        }

//        // DateTime Extensions

//        // Check if a date is a weekend.
//        public static bool IsWeekend(this DateTime date)
//        {
//            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
//        }

//        // Get the age based on a birthdate.
//        public static int Age(this DateTime birthdate)
//        {
//            var age = DateTime.Now.Year - birthdate.Year;
//            if (DateTime.Now < birthdate.AddYears(age)) age--;
//            return age;
//        }

//        // Int Extensions

//        // Check if an integer is even.
//        public static bool IsEven(this int number)
//        {
//            return number % 2 == 0;
//        }

//        // Check if an integer is odd.
//        public static bool IsOdd(this int number)
//        {
//            return number % 2 != 0;
//        }

//        /// <summary>
//        /// Converts a DataTable to a JSON string.
//        /// </summary>
//        /// <param name="dt">The DataTable to convert.</param>
//        /// <returns>A JSON formatted string.</returns>
//        public static string DataTableToJSON(this System.Data.DataTable dt)
//        {
//            var serializer = new JavaScriptSerializer();
//            var rows = new List<Dictionary<string, object>>();
//            foreach (DataRow dr in dt.Rows)
//            {
//                var row = new Dictionary<string, object>();
//                foreach (DataColumn col in dt.Columns)
//                {
//                    row.Add(col.ColumnName, dr[col]);
//                }
//                rows.Add(row);
//            }
//            return serializer.Serialize(rows);
//        }

//        /// <summary>
//        /// Removes potentially harmful HTML tags from a string to prevent XSS attacks.
//        /// </summary>
//        /// <param name="html">The input string containing HTML content.</param>
//        /// <returns>A sanitized string without harmful HTML tags.</returns>
//        public static string SafeXSS(this string html)
//        {
//            return Regex.Replace(
//                html,
//                @"</?(?i:script|embed|object|frameset|frame|iframe|meta|link|style)(.|\n|\s)*?>",
//                string.Empty,
//                RegexOptions.Singleline | RegexOptions.IgnoreCase
//            );
//        }

//        /// <summary>
//        /// Downloads the HTML content of a given URL. Use: string htmlContent = await someUrl.DownloadHTMLAsync();
//        /// </summary>
//        /// <param name="url">The URL to download content from.</param>
//        /// <returns>The HTML content of the specified URL.</returns>
//        public static async Task<string> DownloadHTMLAsync(this string url)
//        {
//            using var httpClient = new HttpClient();
//            return await httpClient.GetStringAsync(url);
//        }
//    }
//}
