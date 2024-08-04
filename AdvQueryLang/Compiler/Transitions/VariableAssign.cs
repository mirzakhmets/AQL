/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/15/2024
 * Time: 2:26 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Assignment operator.
	/// </summary>
	public class VariableAssign : Transition
	{
		public int parameterCount = 0;
		
		public VariableAssign(State begin, State end, int parameterCount)
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
			ArrayList a = new ArrayList();
			
			int n = this.parameterCount;
			
			while (n > 0 && context.stack.Count > 0) {
				--n;
				
				Variable v = context.stack.Pop();
				
				a.Add(v);
			}
			
			a.Reverse();
			
			Variable p = null;
			
			for (int i = a.Count - 1; i > 0; --i) {
				Variable v = (Variable) a[i];
				
				p = (Variable) a[i - 1];
				
				if (p != null) {
					p.SetValue(context, v.GetValue(context));
				}
			}
			
			p = (Variable) a[0];
			
			if (p != null) {
				context.stack.Push(p);
			} else {
				context.stack.Push(new Variables.VarNull());
			}
		}
	}
}
