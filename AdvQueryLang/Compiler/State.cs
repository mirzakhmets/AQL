/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 7:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace AdvQueryLang.Compiler
{
	/// <summary>
	/// State in compiled tree.
	/// </summary>
	public class State
	{
		public ArrayList incoming = new ArrayList();
		public ArrayList outgoing = new ArrayList();
		
		public State()
		{
		}
		
		public virtual bool IsFinal() {
			return false;
		}
		
		public virtual bool Run(Context context) {
			Stack<State> stack = new Stack<State>();
			
			stack.Push(this);
			
			while (stack.Count > 0) {
				State state = stack.Pop();
				
				if (state.IsFinal()) {
					return true;
				}
				
				foreach (object o in state.outgoing) {
					Transition t = (Transition) o;
					
					if (t.Accepts(context)) {
						t.Run(context);
						
						stack.Push(t.end);
					}
				}
			}
			
			return false;
		}
	}
}
