/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 5/14/2024
 * Time: 9:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using AdvQueryLang.Compiler;
using AdvQueryLang.Compiler.Transitions;

namespace AdvQueryLang.Parser.Nodes
{
	/// <summary>
	/// Routine node.
	/// </summary>
	public class Routine : Node
	{
		public Identifier id = null;
		public ArrayList nodes = new ArrayList();
		
		public Routine()
		{
			
		}
		
		public void Add(Node node) {
			if (id == null && node.GetType() == typeof(Identifier)) {
				this.id = (Identifier) node;
			} else {
				this.nodes.Add(node);
			}
		}
		
		public override void Write(System.IO.Stream stream)
		{
			base.Write(stream);
			
			this.Write(stream, "(");
			
			foreach (object node in this.nodes) {
				((Node) node).Write(stream);
				
				this.Write(stream, ", ");
			}
			
			this.Write(stream, ")");
		}
		
		public override Compiler.StatePair Compile() {
			if (this.compiled == null) {
				this.compiled = new StatePair(new State(), new State());
				
				if (this.id != null && this.id.name != null) {
					if (this.id.name.Equals("?")) {
						StatePair statePairTrue = null;
						
						StatePair statePairFalse = null;
						
						StatePair ifPair = ((Node) this.nodes[0]).Compile();
						
						if (this.nodes.Count > 1) {
							Node node = (Node) this.nodes[1];
							
							statePairTrue = node.Compile();
							
							if (this.nodes.Count > 2) {
								node = (Node) this.nodes[2];
								
								statePairFalse = node.Compile();
							}
						}
						
						Compiler.Transition check = new Empty(new State(), ifPair.begin);
						
						new Empty(ifPair.end, new State());
						
						new If(this.compiled.begin, statePairTrue.begin, check);

						new Empty(statePairTrue.begin, this.compiled.end);
						
						if (statePairFalse != null) {
							new IfNot(this.compiled.begin, statePairFalse.begin, check);
							
							new Empty(statePairFalse.begin, this.compiled.end);
						}
						
						return this.compiled;
					} else if (this.id.name.Equals("?$")) {
						StatePair sp = ((Node) this.nodes[0]).Compile();
						
						new Empty(this.compiled.begin, sp.begin);
						
						new Compiler.Transitions.NotNull(sp.end, this.compiled.end);
						
						return this.compiled;
					} else if (this.id.name.Equals("?&")) {						
						Compiler.Transitions.And a = new Compiler.Transitions.And(this.compiled.begin, this.compiled.end);

						foreach (object o in this.nodes) {
							Node node = (Node) o;
							
							StatePair sp = node.Compile();
							
							State state = new State();
							
							a.checks.Add(new Empty(state, sp.begin));
						}
						
						return this.compiled;
					} else if (this.id.name.Equals("?|")) {						
						Compiler.Transitions.Or a = new Compiler.Transitions.Or(this.compiled.begin, this.compiled.end);

						foreach (object o in this.nodes) {
							Node node = (Node) o;
							
							StatePair sp = node.Compile();
							
							State state = new State();
							
							a.checks.Add(new Empty(state, sp.begin));
						}
						
						return this.compiled;
					} else if (this.id.name.Equals("?!")) {
						StatePair sp = ((Node) this.nodes[0]).Compile();
						
						Transition check = new Empty(new State(), sp.begin);
						
						new Compiler.Transitions.Not(this.compiled.begin, this.compiled.end, check);
						
						return this.compiled;
					} else if (this.id.name.Equals("?/")) {
						StatePair spRegex = ((Node) this.nodes[0]).Compile();
						
						StatePair spText = ((Node) this.nodes[1]).Compile();
						
						new Empty(this.compiled.begin, spRegex.begin);
						
						new Empty(spRegex.end, spText.begin);
						
						new Compiler.Transitions.RegexSuccess(spText.end, this.compiled.end);
						
						return this.compiled;
					} else if (this.id.name.Equals("/-")) {
						StatePair spRegex = ((Node) this.nodes[0]).Compile();
						
						StatePair spText = ((Node) this.nodes[1]).Compile();
						
						new Empty(this.compiled.begin, spRegex.begin);
						
						new Empty(spRegex.end, spText.begin);
						
						new Compiler.Transitions.RegexSplit(spText.end, this.compiled.end);
						
						return this.compiled;
					} else if (this.id.name.Equals("/+")) {
						StatePair spRegex = ((Node) this.nodes[0]).Compile();
						
						StatePair spText = ((Node) this.nodes[1]).Compile();
						
						StatePair spValue = ((Node) this.nodes[2]).Compile();
						
						new Empty(this.compiled.begin, spRegex.begin);
						
						new Empty(spRegex.end, spText.begin);

						new Empty(spText.end, spValue.begin);
						
						new Compiler.Transitions.RegexReplace(spValue.end, this.compiled.end);
						
						return this.compiled;
					} else if (this.id.name.Equals("//")) {
						StatePair spRegex = ((Node) this.nodes[0]).Compile();
						
						StatePair spText = ((Node) this.nodes[1]).Compile();
						
						new Empty(this.compiled.begin, spRegex.begin);
						
						new Empty(spRegex.end, spText.begin);
						
						new Compiler.Transitions.RegexMatch(spText.end, this.compiled.end);
						
						return this.compiled;
					} else if (this.id.name.Equals("@")) {
						Compiler.Variables.VarNamed iterator = new Compiler.Variables.VarNamed(((Identifier) this.nodes[0]).name);
						
						StatePair spRange = ((Node) this.nodes[1]).Compile();
						
						StatePair spLoop = ((Node) this.nodes[2]).Compile();
						
						new Loop(this.compiled.begin, this.compiled.end, iterator, new Empty(new State(), spRange.begin), new Empty(new State(), spLoop.begin));
						
						return this.compiled;
					}
				}
				
				State currentState = this.compiled.begin;
				
				State lastState = currentState;
				
				foreach (object o in this.nodes) {
					Node node = (Node) o;
					
					if (node != null) {
						StatePair sp = node.Compile();
						
						if (this.id != null && this.id.name != null) {
							if (this.id.name.Equals("<<")) {
								if (lastState != null) {
									new ConsoleWrite(lastState, sp.begin, 1);
								} else if (lastState != null) {
									new Empty(lastState, sp.begin);
								}
							} else if (lastState != null) {
								new Empty(lastState, sp.begin);
							}
						} else if (lastState != null) {
							new Empty(lastState, sp.begin);
						}
						
						currentState = sp.begin;
						
						lastState = sp.end;
					}
				}
				
				if (this.id != null && this.id.name != null) {
					if (this.id.name.Equals("<<")) {
						new ConsoleWrite(lastState, this.compiled.end, 1);
					} else if (this.id.name.Equals("=")) {
						new VariableAssign(lastState, this.compiled.end, this.nodes.Count);
					} else if (this.id.name.Equals("+")) {
						new Plus(lastState, this.compiled.end, this.nodes.Count);
					} else if (this.id.name.Equals("-")) {
						new Minus(lastState, this.compiled.end, this.nodes.Count);
					} else if (this.id.name.Equals("/")) {
						new Divide(lastState, this.compiled.end, this.nodes.Count);
					} else if (this.id.name.Equals("*")) {
						new Multiply(lastState, this.compiled.end, this.nodes.Count);
					} else {
						new Empty(lastState, this.compiled.end);
					}
				} else {
					new Empty(lastState, this.compiled.end);
				}
			}
			
			return this.compiled;
		}
	}
}
