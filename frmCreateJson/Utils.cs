using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace frmCreateJson
{
    public static class Utils
    {
        public static string AppUser = "vig";
        public static string AppPassword = "vespa";
        public static bool IsAuth = true;
        public static bool ISDevelop = false;

        public static string myStatus = "";

        public static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }

        public static void CleanWorkDir(string aDir)
        {
            var files = from file in Directory.EnumerateFiles(aDir)
                        select file;

            foreach (var file in files)
            {
                File.Delete(file);
            }

        }

        public static void CreaDirectories(string aDir)
        {
            if (!(Directory.Exists(aDir)))
            {
                DirectoryInfo di = Directory.CreateDirectory(aDir);
            }

        }

        public static string GetConnectionStringComplete(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
            {
                returnValue = settings.ConnectionString + "User ID=" + Utils.AppUser + ";Password=" + Utils.AppPassword; ;
            }
            return returnValue;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return true;

            return !enumerable.Any();
        }

        public static string Right(this string str, int length)
        {
            str = (str ?? string.Empty);
            return (str.Length >= length)
                ? str.Substring(str.Length - length, length)
                : str;
        }

        public static string Left(this string str, int length)
        {
            str = (str ?? string.Empty);
            return str.Substring(0, Math.Min(length, str.Length));
        }

        public static string DQuotedStr(this string aString)
        {
            return "\"" + aString + "\"";
        }

        public static string QuotedStr(this string aString)
        {
            return "'" + aString + "'";
        }


        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static bool IsOdd(int value)
        {
            //  Odd è value % 2 != 0
            return value % 2 != 0;
        }
    }
}
