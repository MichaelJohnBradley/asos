using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public static class Guard
    {
        public static void IsNullOrEmpty (string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                throw new ArgumentException($"Value can not be null or empty", param);
            }
        }

        public static void IsEmailValid(string email)
        {
            if (!email.Contains("@") || !email.Contains("."))
            {
                throw new ArgumentException($"Email is not in the correct format", email);
            }
        }
    }
}
