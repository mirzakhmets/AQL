/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 6:43 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;

namespace AdvQueryLang
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Advanced Query Language");
			
			string s = null;
			
			while ((s = Console.ReadLine()) != null && s.Length > 0) {
				try {
					Compiler.Application app = new Compiler.Application(new MemoryStream(Encoding.Default.GetBytes(s)));
					
					app.Run();
				} catch (Exception e) {
					Console.WriteLine(e.Message);
				}
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}