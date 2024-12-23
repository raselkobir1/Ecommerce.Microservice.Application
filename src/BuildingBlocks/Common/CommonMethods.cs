using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class CommonMethods
    {
        private static readonly Random _random = new Random();
        private const string SpecialCharacters = "@#*";

        public static DateTime GetCurrentTime()
        {
            return DateTime.UtcNow.Add(TimeSpan.FromHours(6));
        }
    }
}