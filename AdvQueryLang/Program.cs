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
using Microsoft.Win32;

namespace AdvQueryLang
{
	class Program
	{
		public static void CheckRuns() {
			try {
				RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\OVG-Developers", true);
				
				int runs = -1;
				
				if (key != null) {
					runs = (int) key.GetValue("Runs");
				} else {
					key = Registry.CurrentUser.CreateSubKey("Software\\OVG-Developers");
				}
				
				runs = runs + 1;
				
				key.SetValue("Runs", runs);
				
				if (runs > 10) {
					Console.WriteLine("Number of runs expired.");
					Console.WriteLine("Please register the application (visit https://ovg-developers.mystrikingly.com/ for purchase).");
					
					Environment.Exit(0);
				}
			} catch (Exception e) {
				Console.WriteLine(e.Message);
			}
		}
		
		public static bool IsRegistered() {
			try {
				RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\OVG-Developers");
				
				if (key != null && key.GetValue("Registered") != null) {
					return true;
				}
			} catch (Exception e) {
				Console.WriteLine(e.Message);
			}
			
			return false;
		}
		
		public static void Main(string[] args)
		{
			if (!IsRegistered()) {
				CheckRuns();
			}
			
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