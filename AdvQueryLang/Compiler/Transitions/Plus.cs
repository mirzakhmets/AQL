/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/17/2024
 * Time: 8:03 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Plus operator.
	/// </summary>
	public class Plus : Transition
	{
		public int parameterCount = 0;
		
		public Plus(State begin, State end, int parameterCount)
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
			
			string stringResult = "";
			
			bool isString = false;
			
			foreach (object o in a) {
				Variable v = (Variable) o;
				
				if (v.value is string) {
					isString = true;
				}
				
				if (isString) {
					stringResult += v.value;
				} else {
					result += (double) v.value;
				}
			}
			
			context.stack.Push(new Variables.VarConst(isString ? (object) stringResult : (object) result));
		}
	}
}
