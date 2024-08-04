/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/15/2024
 * Time: 12:29 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Write to console.
	/// </summary>
	public class ConsoleWrite : Transition
	{
		public int parameterCount = 0;
		
		public ConsoleWrite(State begin, State end, int parameterCount)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.parameterCount = parameterCount;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public override bool Accepts(Context context) {
			return true;
		}
		
		public override void Run(Context context) {
			int n = parameterCount;
			
			ArrayList a = new ArrayList();
			
			while (n > 0 && context.stack.Count > 0) {
				--n;
				
				a.Add(context.stack.Pop());
			}
			
			a.Reverse();
			
			Variable result = null;
			
			foreach (object o in a) {
				Variable v = (Variable) o;
				
				Console.WriteLine((result = v).GetValue(context));
			}
			
			context.stack.Push(new Variables.VarConst(result != null ? result.GetValue(context) : new Variables.VarNull()));
		}
	}
}
