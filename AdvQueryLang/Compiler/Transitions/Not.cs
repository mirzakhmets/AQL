/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/17/2024
 * Time: 7:43 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Description of Not.
	/// </summary>
	public class Not : Transition
	{
		public Transition check = null;
		
		public Not(State begin, State end, Transition check)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.check = check;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public override bool Accepts(Context context) {
			check.begin.Run(context);
			
			context.stack.Push(new Variables.VarConst(!((bool) context.stack.Pop().value)));
			
			return true;
		}
		
		public override void Run(Context context)
		{
		}
	}
}
