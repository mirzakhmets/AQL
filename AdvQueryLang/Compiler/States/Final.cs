/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 11:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler.States
{
	/// <summary>
	/// Final state.
	/// </summary>
	public class Final : State
	{
		public Final()
		{
		}
		
		public override bool IsFinal() {
			return true;
		}
	}
}
