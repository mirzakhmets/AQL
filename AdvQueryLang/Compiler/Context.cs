/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 9:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace AdvQueryLang.Compiler
{
	/// <summary>
	/// Running context.
	/// </summary>
	public class Context
	{
		public Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
		public Stack<Variable> stack = new Stack<Variable>();
		
		public Context()
		{
		}
		
		public Context(Context parent) {
			this.stack = parent.stack;
		}
		
		public Variable GetNamed(string name, object value) {
			if (variables.ContainsKey(name)) {
				return variables[name];
			}
			
			Variable result;
			
			variables.Add(name, result = new Variables.VarNamed(name));
			
			result.value = value;
			
			return result;
		}
		
		public object GetValue(string name) {
			return this.GetNamed(name, null).value;
		}
		
		public void SetValue(string name, object value) {
			this.GetNamed(name, value).value = value;
		}
	}
}
