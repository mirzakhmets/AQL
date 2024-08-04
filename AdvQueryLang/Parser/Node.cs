/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 7:57 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;
using AdvQueryLang.Compiler.Transitions;

namespace AdvQueryLang.Parser
{
	/// <summary>
	/// Tree node.
	/// </summary>
	public class Node
	{
		public Compiler.StatePair compiled = null;
		
		public Node()
		{
		}
		
		public virtual Compiler.StatePair Compile() {
			if (this.compiled == null) {
				this.compiled = new Compiler.StatePair(new Compiler.State(), new Compiler.State());
				
				new Empty(this.compiled.begin, this.compiled.end);
			}
			
			return this.compiled;
		}
		
		public virtual void Write(Stream stream) {
		}
		
		public void Write(Stream stream, string text) {
			byte[] bytes = Encoding.Default.GetBytes(text);
			
			stream.Write(bytes, 0, bytes.Length);
		}
	}
}
