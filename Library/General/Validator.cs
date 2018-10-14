using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Library.General
{
    public class Validator
    {
        public static bool IsOnlyLetters(string input)
        {
            return !(Regex.Matches(input, @"[a-zA-Z]").Count != input.Length);
        }

        public static bool IsOnlyNumbers(string input)
        {
            foreach (char c in input)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public static bool IsValidGender(string gender)
        {
            return gender == "M" || gender == "F";
        }
    }
}