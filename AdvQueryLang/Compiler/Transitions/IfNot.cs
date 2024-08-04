/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/17/2024
 * Time: 10:26 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Logical IfNot.
	/// </summary>
	public class IfNot : Transition
	{
		public Transition check = null;
		
		public IfNot(State begin, State end, Transition check)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.check = check;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public override bool Accepts(Context context) {
			check.begin.Run(context);
			
			return ! ((bool) context.stack.Pop().value);
		}
		
		public override void Run(Context context)
		{
		}
	}
}
