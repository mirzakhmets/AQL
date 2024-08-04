/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/15/2024
 * Time: 12:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Custom value.
	/// </summary>
	public class CustomValue : Transition
	{
		public object value = null;
		
		public CustomValue(State begin, State end, object value)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.value = value;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public override bool Accepts(Context context) {
			return true;
		}
		
		public override void Run(Context context) {
			context.stack.Push(new Variables.VarConst(this.value));
		}
	}
}
