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

			try
			{
				string[] result = SeparateWords(separators, path).ToArray();
			}
			catch (Exception exception) { Console.WriteLine(exception.Message); }
			
		}

		public static IEnumerable<string> SeparateWords(char[] separators, string path)
		{

			StringBuilder builder = new();
			Dictionary<string, int> words = new();

			try
			{
				using (FileStream stream = File.OpenRead(path))
				{
					StreamReader reader = new(stream, Encoding.UTF8);
					int i = 0;
					while (i++ != stream.Length - 1)
					{
						char[] temp = new char[1];
						reader.Read(temp, 0, 1);
						if (temp[0] != '\0')
						{
							if (separators.Contains(temp[0]))
							{
								if (words.Keys.Contains(builder.ToString())) words[builder.ToString().ToUpper()] += 1;
								else words[builder.ToString().ToUpper()] = 1;

								builder.Clear();
								continue;
							}
							builder.Append(temp[0]);
						}
					}
				}
			}
			catch (DirectoryNotFoundException) { Console.WriteLine("Directory not found. Enter a valid path to one."); yield break; }
			catch (FileNotFoundException) { Console.WriteLine("File not found. Enter a valid path to one."); yield break; }

			var results = words.Where(item => Regex.IsMatch(item.Key, @"^[а-яА-Я]+$")).OrderByDescending(item => item.Value).ThenBy(item => item.Key).ToArray();
			foreach (var result in results)
			{
				yield return $"{result.Key}: {words[result.Key]}";
			}
		}
	}
}
