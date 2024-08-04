﻿/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 9:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using AdvQueryLang.Compiler;
using AdvQueryLang.Compiler.Transitions;

namespace AdvQueryLang.Parser.Nodes
{
	/// <summary>
	/// Logical node.
	/// </summary>
	public class Logical : Node
	{
		public bool value = false;
		
		public Logical(bool value)
		{
			this.value = value;
		}
		
		public override void Write(System.IO.Stream stream)
		{
			base.Write(stream);
			
			this.Write(stream, "" + value);
		}
		
		public override Compiler.StatePair Compile() {
			if (this.compiled == null) {
				this.compiled = new StatePair(new State(), new State());
				
				new CustomValue(this.compiled.begin, this.compiled.end, this.value);
			}
			
			return this.compiled;
		}
	}
}
