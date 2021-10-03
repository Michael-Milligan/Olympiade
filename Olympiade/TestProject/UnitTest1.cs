using System.IO;
using HTML_parser;
using NUnit.Framework;

namespace TestProject
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		[TestCase("file1")]
		[TestCase("file2")]
		[TestCase("file3")]
		public void GetDataTest(string _path)
		{
			string path = $"D:\\{_path}.html";
			Assert.AreEqual(File.ReadAllText(path), Program.GetData(path));
		}
	}
}