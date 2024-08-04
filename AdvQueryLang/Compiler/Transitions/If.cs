/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 11:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Loical If.
	/// </summary>
	public class If : Transition
	{
		public Transition check = null;
		
		public If(State begin, State end, Transition check)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.check = check;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public override bool Accepts(Context context) {
			check.begin.Run(context);
			
			return (bool) context.stack.Pop().value;
		}
		
		public override void Run(Context context)
		{
		}
	}
}
