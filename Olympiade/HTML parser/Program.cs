using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace HTML_parser
{
	class Program
	{
		static void Main()
		{
			string path = "";
			string inputData = File.ReadAllText(path);
			char[] separators = { ' ', '<', '>' };

			StringBuilder builder = new();
			Dictionary<string, int> words = new();

			for (int i = 0; i < inputData.Length; ++i)
			{
				if (separators.Contains(inputData[i]))
				{
					if (words.Keys.Contains(builder.ToString())) words[builder.ToString()] += 1;
					else words[builder.ToString()] = 1;

					builder.Clear();
					continue;
				}
				builder.Append(inputData[i]);
			}
			var results = words.OrderByDescending(item => item.Key).ToArray();
			foreach (var result in results)
			{
				Console.WriteLine($"{result.Key}: {words[result.Key]}");
			}
		}
	}
}
