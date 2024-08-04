/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 8:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using AdvQueryLang.Parser;

namespace AdvQueryLang.Compiler
{
	/// <summary>
	/// Main application.
	/// </summary>
	public class Application
	{
		public ParsingStream stream = null;
		public Context globalContext = new Context();
		
		public Application(Stream _stream)
		{
			this.stream = new ParsingStream(_stream);
		}
		
		public void Run() {
			Node node = this.stream.ParseNode();
			
			if (node != null) {
				StatePair statePair = node.Compile();
				
				if (statePair == null) {
					throw new Exception("Not compiled");
				}

				statePair.begin.Run(this.globalContext);
			} else {
				throw new Exception("Node is empty");
			}
		}
	}
}
