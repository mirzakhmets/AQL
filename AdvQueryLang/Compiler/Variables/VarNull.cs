/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/15/2024
 * Time: 11:27 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler.Variables
{
	/// <summary>
	/// Empty variable.
	/// </summary>
	public class VarNull : Variable
	{
		public VarNull()
		{
		}
		
		public override object GetValue(Context context)
		{
			return null;
		}
	}
}
