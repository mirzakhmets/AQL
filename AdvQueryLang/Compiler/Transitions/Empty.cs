/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 11:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler.Transitions
{
	/// <summary>
	/// Empty transition.
	/// </summary>
	public class Empty : Transition
	{
		public Empty(State begin, State end)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public override bool IsEmpty() {
			return true;
		}
		
		public override bool Accepts(Context context) {
			return true;
		}
	}
}
