// Decompiled with JetBrains decompiler
// Type: CSVdb.ParsingStream
// Assembly: CSVdb, Version=1.0.8101.38144, Culture=neutral, PublicKeyToken=null
// MVID: 61508883-B98E-436B-8951-CECEB0656FB9
// Assembly location: C:\CSVdb\CSVdb.exe

using System;
using System.IO;

namespace AdvQueryLang.Parser
{
  public class ParsingStream
  {
    public Stream stream;
    public int current = -1;

    public ParsingStream(Stream stream)
    {
      this.stream = stream;
      this.Read();
    }

    public void Read() {
    	this.current = this.stream.ReadByte();
    }

    public bool AtEnd() {
    	return this.current == -1;
    }

    public void ParseBlanks()
    {
    	while (!this.AtEnd() && " \r\t\v".IndexOf((char) this.current) >= 0) this.Read();
    }
    
    public Node ParseIdentifier() {
    	string value = "";
    	
    	while (!this.AtEnd() && "(),;\"'".IndexOf((char) this.current) < 0) {
    		if (this.current == (int) '\\') {
    			this.Read();
    			
    			if (this.AtEnd()) {
    				value += '\\';
    				
    				break;
    			}
    		}
    		
    		value += (char) this.current;
    		
    		this.Read();
    	}
    	
    	if (value.Length > 0) {
	    	if (value.ToLower().Equals("true")) {
	    		return new Nodes.Logical(true);
	    	}
	    	
	    	if (value.ToLower().Equals("false")) {
	    		return new Nodes.Logical(false);
	    	}
    		
    		double result = 0;
    		
    		if (double.TryParse(value, out result)) {
    			return new Nodes.Numeric(result);
	    	}
    		
    		return new Nodes.Identifier(value);
    	}
    	
    	return null;
    }
    
    public Node ParseLiteral() {
    	this.ParseBlanks();
    	
    	string value = "";
    	
    	if (!this.AtEnd() && "\"'".IndexOf((char) this.current) >= 0) {
    		char delimeter = (char) this.current;
    		
    		this.Read();
    		
    		while (!this.AtEnd() && (char) this.current != delimeter) {
    			if (this.current == (int) '\\') {
	    			this.Read();
	    		}
    			
    			value += (char) this.current;
    			this.Read();
    		}
    		
    		if (!this.AtEnd()) {
    			this.Read();
    		}
    		
    		this.ParseBlanks();
    		
    		return new Nodes.Literal(value);
    	}
    	
    	this.ParseBlanks();
    	
    	return null;
    }
    
    public Node ParseRoutine() {
    	this.ParseBlanks();
    	
    	char delimeter = '\0';
    	
    	if (this.current == (int) '(') {
    		this.Read();
    		
    		delimeter = ')';
    	} else if (this.current == (int) '[') {
    		this.Read();
    		
    		delimeter = ']';
    	} else if (this.current == (int) '{') {
    		this.Read();
    		
    		delimeter = '}';
    	}
    	
    	if (delimeter == '\0') {		    	
	    	Node resultb = this.ParseLiteral();
	    	
	    	if (resultb != null) {
	    		return resultb;
	    	}
	    	
	    	resultb = this.ParseIdentifier();
	    	
	    	if (resultb != null) {
	    		return resultb;
	    	}
	    	
	    	return null;
    	}
    	
		Nodes.Routine result = new Nodes.Routine();
		
		while (!this.AtEnd() && (this.current != (int) delimeter)) {
			Node node = this.ParseNode();
			
			if (node != null) {
				result.Add(node);
			}
			
			this.ParseBlanks();
			
			if (!this.AtEnd() && ",;".IndexOf((char) this.current) >= 0) {
				this.Read();
			}
			
			this.ParseBlanks();
		}
		
		this.ParseBlanks();
		
		if (this.current == (int) delimeter) {
			this.Read();
		}
		
		this.ParseBlanks();
		
	    return result;
    }
    
    public Node ParseNode() {
    	this.ParseBlanks();
    	
    	Node result = this.ParseRoutine();
    	
    	this.ParseBlanks();
    	
    	return result;
    }
  }
}
