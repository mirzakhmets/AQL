/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/15/2024
 * Time: 10:23 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler.Variables
{
	/// <summary>
	/// Named variable.
	/// </summary>
	public class VarNamed : Variable
	{
		public string name = null;
		
		public VarNamed(string name)
		{
			this.name = name;
		}
		
		public override object GetValue(Context context) {
			return context.GetNamed(this.name, this.value).value;
		}
		
		public override void SetValue(Context context, object value)
		{
			context.GetNamed(this.name, value).value = value;
		}
	}
}
