/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/15/2024
 * Time: 12:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AdvQueryLang.Compiler
{
	/// <summary>
	/// Compiled variable.
	/// </summary>
	public class Variable
	{
		public object value = null;
		
		public Variable()
		{
			
		}
		
		public virtual object GetValue(Context context) {
			return this.value;
		}
		
		public virtual void SetValue(Context context, object value) {
			this.value = value;
		}
	}
}
