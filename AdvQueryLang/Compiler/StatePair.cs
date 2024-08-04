/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 7:59 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler
{
	/// <summary>
	/// Pair of states.
	/// </summary>
	public class StatePair
	{
		public State begin, end;
		
		public StatePair(State begin, State end)
		{
			this.begin = begin;
			
			this.end = end;
		}
	}
}
