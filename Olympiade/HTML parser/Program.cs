using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Management;

namespace HTML_parser
{
	public class Program
	{
		static void Main()
		{
			string path = "D:\\file2.html";
			char[] separators = { ' ', '<', '>' };

			string data = GetData(path);
			string[] result = SeparateWords(separators, data).ToArray();
			
		}

		public static string GetData(string path)
		{
			string result = "";
			using (FileStream stream = File.OpenRead(path))
			{
				StreamReader reader = new(stream, Encoding.UTF8);
				int i = 0;
				while (i++ != stream.Length)
				{
					char[] temp = new char[1];
					reader.Read(temp, 0, 1);
					if (temp[0] != '\0') result += temp[0];
				}
			}
			return result;
		}

		public static IEnumerable<string> SeparateWords(char[] separators, string inputData)
		{

			StringBuilder builder = new();
			Dictionary<string, int> words = new();

			for (int i = 0; i < inputData.Length; ++i)
			{
				if (separators.Contains(inputData[i]))
				{
					if (words.Keys.Contains(builder.ToString())) words[builder.ToString().ToUpper()] += 1;
					else words[builder.ToString().ToUpper()] = 1;

					builder.Clear();
					continue;
				}
				builder.Append(inputData[i]);
			}
			var results = words.Where(item => Regex.IsMatch(item.Key, @"^[а-яА-Я]+$")).OrderByDescending(item => item.Value).ThenBy(item => item.Key).ToArray();
			foreach (var result in results)
			{
				yield return $"{result.Key}: {words[result.Key]}";
			}
		}
	}
}
