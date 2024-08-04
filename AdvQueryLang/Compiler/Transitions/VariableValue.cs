/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/15/2024
 * Time: 12:26 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Get value of variable.
	/// </summary>
	public class VariableValue : Transition
	{
		public string name = null;
		
		public VariableValue(State begin, State end, string name)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.name = name;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public override bool Accepts(Context context) {
			return true;
		}
		
		public override void Run(Context context) {
			Variable result = context.GetNamed(this.name, null);
			
			result.SetValue(context, result.value);

			context.stack.Push(result);
		}
	}
}
