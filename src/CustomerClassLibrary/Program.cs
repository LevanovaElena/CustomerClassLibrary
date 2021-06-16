using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CustomerClassLibrary
{
    public class Program
    {
        class Person
        {

        }
        public static void Main(string[] args)
        {

            List<string> list = new List<string>();
            list.Add("str1");
            list.Add("str2");
            string json = JsonSerializer.Serialize<List<string>>(list);
            Console.WriteLine(json);
            list = JsonSerializer.Deserialize<List<string>>(json);
            Console.WriteLine(list);
        }
    }
}
