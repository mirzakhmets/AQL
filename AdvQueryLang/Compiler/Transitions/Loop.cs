/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/17/2024
 * Time: 8:56 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Loop transition.
	/// </summary>
	public class Loop : Transition
	{
		public int stackBefore = 0;
		
		public Variables.VarNamed iterator = null;
		
		public Transition loop = null;
		
		public Transition range = null;
		
		public Loop(State begin, State end, Variables.VarNamed iterator, Transition range, Transition loop)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.iterator = iterator;
			
			this.range = range;
			
			this.loop = loop;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public override bool Accepts(Context context)
		{
			this.stackBefore = context.stack.Count;
			
			if (range.Accepts(context)) {
				range.begin.Run(context);
			}
			
			return true;
		}
		
		public override void Run(Context context) {
			ArrayList a = new ArrayList();

			while (context.stack.Count > this.stackBefore) {
				a.Add(context.stack.Pop());
			}
			
			a.Reverse();
			
			foreach (object o in a) {
				Variable v = (Variable) o;
				
				context.SetValue (iterator.name, v.GetValue(context));
				
				iterator.SetValue(context, v.GetValue(context));
				
				int n = context.stack.Count;
				
				this.loop.begin.Run(context);
				
				while (context.stack.Count > n) {
					context.stack.Pop();
				}
			}
		}
	}
}
