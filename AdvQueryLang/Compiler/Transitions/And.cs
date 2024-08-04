/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/17/2024
 * Time: 7:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// And operator.
	/// </summary>
	public class And : Transition
	{
		public ArrayList checks = new ArrayList();
		
		public And(State begin, State end)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public override bool Accepts(Context context) {
			foreach (object o in this.checks) {
				Transition t = (Transition) o;
				
				t.begin.Run(context);
				
				if (! ((bool) context.stack.Pop().value)) {
					context.stack.Push(new Variables.VarConst(false));
					return true;
				}
			}

			context.stack.Push(new Variables.VarConst(true));			
			return true;
		}
		
		public override void Run(Context context)
		{
		}
	}
}
