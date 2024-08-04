/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/17/2024
 * Time: 10:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text.RegularExpressions;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Split string.
	/// </summary>
	public class RegexSplit : Transition
	{
		public RegexSplit(State begin, State end)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public override bool Accepts(Context context) {
			return true;
		}
		
		public override void Run(Context context)
		{
			string text = "" + context.stack.Pop().value;
			
			string pattern = "" + context.stack.Pop().value;
			
			string[] sr = Regex.Split(text, pattern);
			
			foreach (string s in sr) {
				context.stack.Push(new Variables.VarConst(s));
			}
		}
	}
}
