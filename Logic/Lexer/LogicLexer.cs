//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Logic/Grammar/Logic.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Fictoria.Logic.Lexer {
using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public partial class LogicLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, STRING=20, IF=21, ELSE=22, IDENTIFIER=23, INT=24, 
		FLOAT=25, ARROW=26, AT=27, AMP=28, TILDE=29, QUESTION=30, WILDCARD=31, 
		EQUALS=32, OPEN_PAREN=33, CLOSE_PAREN=34, OPEN_BRACK=35, CLOSE_BRACK=36, 
		OPEN_BRACE=37, CLOSE_BRACE=38, PIPE=39, COLON=40, SEMICOLON=41, COMMA=42, 
		PERIOD=43, WHITESPACE=44, LINE_COMMENT=45;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "STRING", "IF", "ELSE", "IDENTIFIER", "INT", "FLOAT", 
		"ARROW", "AT", "AMP", "TILDE", "QUESTION", "WILDCARD", "EQUALS", "OPEN_PAREN", 
		"CLOSE_PAREN", "OPEN_BRACK", "CLOSE_BRACK", "OPEN_BRACE", "CLOSE_BRACE", 
		"PIPE", "COLON", "SEMICOLON", "COMMA", "PERIOD", "WHITESPACE", "LINE_COMMENT"
	};


	public LogicLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public LogicLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'instance'", "'-'", "'!'", "'::'", "'^'", "'*'", "'/'", "'+'", 
		"'>'", "'<'", "'>='", "'<='", "'=='", "'!='", "'and'", "'or'", "'xor'", 
		"'true'", "'false'", null, "'if'", "'else'", null, null, null, "'->'", 
		"'@'", "'&'", "'~'", "'?'", "'_'", "'='", "'('", "')'", "'['", "']'", 
		"'{'", "'}'", "'|'", "':'", "';'", "','", "'.'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, "STRING", "IF", "ELSE", 
		"IDENTIFIER", "INT", "FLOAT", "ARROW", "AT", "AMP", "TILDE", "QUESTION", 
		"WILDCARD", "EQUALS", "OPEN_PAREN", "CLOSE_PAREN", "OPEN_BRACK", "CLOSE_BRACK", 
		"OPEN_BRACE", "CLOSE_BRACE", "PIPE", "COLON", "SEMICOLON", "COMMA", "PERIOD", 
		"WHITESPACE", "LINE_COMMENT"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Logic.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static LogicLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,45,248,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,7,33,2,34,7,34,2,35,
		7,35,2,36,7,36,2,37,7,37,2,38,7,38,2,39,7,39,2,40,7,40,2,41,7,41,2,42,
		7,42,2,43,7,43,2,44,7,44,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,
		2,1,2,1,3,1,3,1,3,1,4,1,4,1,5,1,5,1,6,1,6,1,7,1,7,1,8,1,8,1,9,1,9,1,10,
		1,10,1,10,1,11,1,11,1,11,1,12,1,12,1,12,1,13,1,13,1,13,1,14,1,14,1,14,
		1,14,1,15,1,15,1,15,1,16,1,16,1,16,1,16,1,17,1,17,1,17,1,17,1,17,1,18,
		1,18,1,18,1,18,1,18,1,18,1,19,1,19,5,19,156,8,19,10,19,12,19,159,9,19,
		1,19,1,19,1,20,1,20,1,20,1,21,1,21,1,21,1,21,1,21,1,22,1,22,5,22,173,8,
		22,10,22,12,22,176,9,22,1,23,3,23,179,8,23,1,23,4,23,182,8,23,11,23,12,
		23,183,1,24,4,24,187,8,24,11,24,12,24,188,1,24,1,24,4,24,193,8,24,11,24,
		12,24,194,1,25,1,25,1,25,1,26,1,26,1,27,1,27,1,28,1,28,1,29,1,29,1,30,
		1,30,1,31,1,31,1,32,1,32,1,33,1,33,1,34,1,34,1,35,1,35,1,36,1,36,1,37,
		1,37,1,38,1,38,1,39,1,39,1,40,1,40,1,41,1,41,1,42,1,42,1,43,1,43,1,43,
		1,43,1,44,1,44,1,44,1,44,5,44,242,8,44,10,44,12,44,245,9,44,1,44,1,44,
		0,0,45,1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,21,11,23,12,25,13,
		27,14,29,15,31,16,33,17,35,18,37,19,39,20,41,21,43,22,45,23,47,24,49,25,
		51,26,53,27,55,28,57,29,59,30,61,31,63,32,65,33,67,34,69,35,71,36,73,37,
		75,38,77,39,79,40,81,41,83,42,85,43,87,44,89,45,1,0,6,1,0,34,34,2,0,65,
		90,97,122,4,0,48,57,65,90,95,95,97,122,1,0,48,57,3,0,9,10,13,13,32,32,
		2,0,10,10,13,13,254,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,
		9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,
		0,0,0,21,1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,
		31,1,0,0,0,0,33,1,0,0,0,0,35,1,0,0,0,0,37,1,0,0,0,0,39,1,0,0,0,0,41,1,
		0,0,0,0,43,1,0,0,0,0,45,1,0,0,0,0,47,1,0,0,0,0,49,1,0,0,0,0,51,1,0,0,0,
		0,53,1,0,0,0,0,55,1,0,0,0,0,57,1,0,0,0,0,59,1,0,0,0,0,61,1,0,0,0,0,63,
		1,0,0,0,0,65,1,0,0,0,0,67,1,0,0,0,0,69,1,0,0,0,0,71,1,0,0,0,0,73,1,0,0,
		0,0,75,1,0,0,0,0,77,1,0,0,0,0,79,1,0,0,0,0,81,1,0,0,0,0,83,1,0,0,0,0,85,
		1,0,0,0,0,87,1,0,0,0,0,89,1,0,0,0,1,91,1,0,0,0,3,100,1,0,0,0,5,102,1,0,
		0,0,7,104,1,0,0,0,9,107,1,0,0,0,11,109,1,0,0,0,13,111,1,0,0,0,15,113,1,
		0,0,0,17,115,1,0,0,0,19,117,1,0,0,0,21,119,1,0,0,0,23,122,1,0,0,0,25,125,
		1,0,0,0,27,128,1,0,0,0,29,131,1,0,0,0,31,135,1,0,0,0,33,138,1,0,0,0,35,
		142,1,0,0,0,37,147,1,0,0,0,39,153,1,0,0,0,41,162,1,0,0,0,43,165,1,0,0,
		0,45,170,1,0,0,0,47,178,1,0,0,0,49,186,1,0,0,0,51,196,1,0,0,0,53,199,1,
		0,0,0,55,201,1,0,0,0,57,203,1,0,0,0,59,205,1,0,0,0,61,207,1,0,0,0,63,209,
		1,0,0,0,65,211,1,0,0,0,67,213,1,0,0,0,69,215,1,0,0,0,71,217,1,0,0,0,73,
		219,1,0,0,0,75,221,1,0,0,0,77,223,1,0,0,0,79,225,1,0,0,0,81,227,1,0,0,
		0,83,229,1,0,0,0,85,231,1,0,0,0,87,233,1,0,0,0,89,237,1,0,0,0,91,92,5,
		105,0,0,92,93,5,110,0,0,93,94,5,115,0,0,94,95,5,116,0,0,95,96,5,97,0,0,
		96,97,5,110,0,0,97,98,5,99,0,0,98,99,5,101,0,0,99,2,1,0,0,0,100,101,5,
		45,0,0,101,4,1,0,0,0,102,103,5,33,0,0,103,6,1,0,0,0,104,105,5,58,0,0,105,
		106,5,58,0,0,106,8,1,0,0,0,107,108,5,94,0,0,108,10,1,0,0,0,109,110,5,42,
		0,0,110,12,1,0,0,0,111,112,5,47,0,0,112,14,1,0,0,0,113,114,5,43,0,0,114,
		16,1,0,0,0,115,116,5,62,0,0,116,18,1,0,0,0,117,118,5,60,0,0,118,20,1,0,
		0,0,119,120,5,62,0,0,120,121,5,61,0,0,121,22,1,0,0,0,122,123,5,60,0,0,
		123,124,5,61,0,0,124,24,1,0,0,0,125,126,5,61,0,0,126,127,5,61,0,0,127,
		26,1,0,0,0,128,129,5,33,0,0,129,130,5,61,0,0,130,28,1,0,0,0,131,132,5,
		97,0,0,132,133,5,110,0,0,133,134,5,100,0,0,134,30,1,0,0,0,135,136,5,111,
		0,0,136,137,5,114,0,0,137,32,1,0,0,0,138,139,5,120,0,0,139,140,5,111,0,
		0,140,141,5,114,0,0,141,34,1,0,0,0,142,143,5,116,0,0,143,144,5,114,0,0,
		144,145,5,117,0,0,145,146,5,101,0,0,146,36,1,0,0,0,147,148,5,102,0,0,148,
		149,5,97,0,0,149,150,5,108,0,0,150,151,5,115,0,0,151,152,5,101,0,0,152,
		38,1,0,0,0,153,157,5,34,0,0,154,156,8,0,0,0,155,154,1,0,0,0,156,159,1,
		0,0,0,157,155,1,0,0,0,157,158,1,0,0,0,158,160,1,0,0,0,159,157,1,0,0,0,
		160,161,5,34,0,0,161,40,1,0,0,0,162,163,5,105,0,0,163,164,5,102,0,0,164,
		42,1,0,0,0,165,166,5,101,0,0,166,167,5,108,0,0,167,168,5,115,0,0,168,169,
		5,101,0,0,169,44,1,0,0,0,170,174,7,1,0,0,171,173,7,2,0,0,172,171,1,0,0,
		0,173,176,1,0,0,0,174,172,1,0,0,0,174,175,1,0,0,0,175,46,1,0,0,0,176,174,
		1,0,0,0,177,179,5,45,0,0,178,177,1,0,0,0,178,179,1,0,0,0,179,181,1,0,0,
		0,180,182,7,3,0,0,181,180,1,0,0,0,182,183,1,0,0,0,183,181,1,0,0,0,183,
		184,1,0,0,0,184,48,1,0,0,0,185,187,7,3,0,0,186,185,1,0,0,0,187,188,1,0,
		0,0,188,186,1,0,0,0,188,189,1,0,0,0,189,190,1,0,0,0,190,192,3,85,42,0,
		191,193,7,3,0,0,192,191,1,0,0,0,193,194,1,0,0,0,194,192,1,0,0,0,194,195,
		1,0,0,0,195,50,1,0,0,0,196,197,5,45,0,0,197,198,5,62,0,0,198,52,1,0,0,
		0,199,200,5,64,0,0,200,54,1,0,0,0,201,202,5,38,0,0,202,56,1,0,0,0,203,
		204,5,126,0,0,204,58,1,0,0,0,205,206,5,63,0,0,206,60,1,0,0,0,207,208,5,
		95,0,0,208,62,1,0,0,0,209,210,5,61,0,0,210,64,1,0,0,0,211,212,5,40,0,0,
		212,66,1,0,0,0,213,214,5,41,0,0,214,68,1,0,0,0,215,216,5,91,0,0,216,70,
		1,0,0,0,217,218,5,93,0,0,218,72,1,0,0,0,219,220,5,123,0,0,220,74,1,0,0,
		0,221,222,5,125,0,0,222,76,1,0,0,0,223,224,5,124,0,0,224,78,1,0,0,0,225,
		226,5,58,0,0,226,80,1,0,0,0,227,228,5,59,0,0,228,82,1,0,0,0,229,230,5,
		44,0,0,230,84,1,0,0,0,231,232,5,46,0,0,232,86,1,0,0,0,233,234,7,4,0,0,
		234,235,1,0,0,0,235,236,6,43,0,0,236,88,1,0,0,0,237,238,5,47,0,0,238,239,
		5,47,0,0,239,243,1,0,0,0,240,242,8,5,0,0,241,240,1,0,0,0,242,245,1,0,0,
		0,243,241,1,0,0,0,243,244,1,0,0,0,244,246,1,0,0,0,245,243,1,0,0,0,246,
		247,6,44,1,0,247,90,1,0,0,0,8,0,157,174,178,183,188,194,243,2,0,1,0,6,
		0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Fictoria.Logic.Lexer
