using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
class Program {
	private static int Main(string[] args) {
		bool isReverse = false;
		bool hasError = false;
		Encoding encoding = Encoding.Default;
		string inputFilePath = null;
		string outputFilePath = null;

		for(int i = 0; i < args.Length; i++) {
			string arg = args[i];
			switch(arg) {
				case "-reverse":
					isReverse = true;
					break;
				case "-encoding":
					i += 1;
					if(i < args.Length) {
						try {
							encoding = Encoding.GetEncoding(args[i]);
						} catch(Exception e) {
							Console.Error.WriteLine(e.Message);
							hasError = true;
						}
					} else {
						hasError = true;
						Console.Error.WriteLine("no encoding...");
					}
					break;
				default:
					if(inputFilePath == null) {
						inputFilePath = arg;
					} else if(outputFilePath == null) {
						outputFilePath = arg;
					} else {
						hasError = true;
						Console.Error.WriteLine("too many arguments...");
					}
					break;
			}
		}
		if(hasError) {
			return -1;
		} else {
			String inputText, convertedText;
			if(inputFilePath == null) {
				using(Stream iStream = Console.OpenStandardInput())
				using(StreamReader sReader = new StreamReader(iStream, encoding)) {
					inputText = sReader.ReadToEnd();
				}
			} else {
				using(FileStream fStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using(StreamReader sReader = new StreamReader(fStream, encoding)) {
					inputText = sReader.ReadToEnd();
				}
			}

			if(isReverse) {
				convertedText = Regex.Replace(inputText, @"\\u([0-9a-fA-F]{4})",delegate(Match m) {
					int n = Convert.ToInt32(m.Groups[1].Value, 16);
					char c = (char)n;
					return c.ToString();
				});
			} else {
				StringBuilder sb = new StringBuilder();
				foreach(char c in inputText) {
					if(c <= '\u007F') {
						sb.Append(c);
					} else {
						sb.Append(string.Format(@"\u{0:x4}", Convert.ToInt32(c)));
					}
				}
				convertedText = sb.ToString();
			}

			if(outputFilePath == null) {
				using(Stream oStream = Console.OpenStandardOutput())
				using(StreamWriter sWriter = new StreamWriter(oStream, encoding)) {
					sWriter.Write(convertedText);
				}
			} else {
				using(FileStream fStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
				using(StreamWriter sWriter = new StreamWriter(fStream, encoding)) {
					sWriter.Write(convertedText);
				}
			}
			return 0;
		}
	}
}
