/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/17/2024
 * Time: 8:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Division operator.
	/// </summary>
	public class Divide : Transition
	{
		public int parameterCount = 0;
		
		public Divide(State begin, State end, int parameterCount)
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
			
			double result = 0;
			
			bool first = true;
			
			foreach (object o in a) {
				Variable v = (Variable) o;
				
				if (first) {
					result = (double) v.value;
				} else {
					result /= (double) v.value;
				}
				
				first = false;
			}
			
			context.stack.Push(new Variables.VarConst(result));
		}
	}
}
