/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/15/2024
 * Time: 10:18 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler.Variables
{
	/// <summary>
	/// Constant value.
	/// </summary>
	public class VarConst : Variable
	{
		public VarConst(object value)
		{
			this.value = value;
		}
	}
}
