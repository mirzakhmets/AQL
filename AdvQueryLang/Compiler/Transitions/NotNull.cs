/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/17/2024
 * Time: 6:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Not null check.
	/// </summary>
	public class NotNull : Transition
	{
		public NotNull(State begin, State end)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public override bool Accepts(Context context) {
			return true;
		}
		
		public override void Run(Context context) {
			context.stack.Push(new Variables.VarConst(context.stack.Pop().value != null));
		}
	}
}
