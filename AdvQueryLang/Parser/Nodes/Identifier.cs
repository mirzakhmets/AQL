/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 9:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using AdvQueryLang.Compiler;
using AdvQueryLang.Compiler.Transitions;

namespace AdvQueryLang.Parser.Nodes
{
	/// <summary>
	/// Identifier node.
	/// </summary>
	public class Identifier : Node
	{
		public string name = null;
		
		public Identifier(string name)
		{
			this.name = name;
		}
		
		public override void Write(System.IO.Stream stream)
		{
			base.Write(stream);
			
			this.Write(stream, this.name);
		}
		
		public override Compiler.StatePair Compile() {
			if (this.compiled == null) {
				this.compiled = new StatePair(new State(), new State());
				
				new VariableValue(this.compiled.begin, this.compiled.end, this.name);
			}
			
			return this.compiled;
		}
	}
}
