/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 7:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler
{
	/// <summary>
	/// Compiled transition.
	/// </summary>
	public class Transition
	{
		public State begin = null, end = null;
		
		public Transition() {
			
		}
		
		public Transition(State begin, State end)
		{
			this.begin = begin;
			
			this.end = end;
			
			this.begin.outgoing.Add(this);
			
			this.end.incoming.Add(this);
		}
		
		public virtual bool IsEmpty() {
			return false;
		}
		
		public virtual bool Accepts(Context context) {
			return false;
		}
		
		public virtual void Run(Context context) {
		}
	}
}
