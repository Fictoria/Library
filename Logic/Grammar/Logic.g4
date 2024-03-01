grammar Logic;

logic
    :   statement+ EOF
    ;

statement
    :   type
    |   instance
    |   schema
    |   fact
    |   antifact
    |   function
    |   action
    |   query
    ;

query
    :   expression QUESTION
    ;

type
    :   identifier COLON identifier (COMMA identifier)*
        PERIOD
    ;

instance
    :   'instantiate' OPEN_PAREN name=expression COMMA of=expression CLOSE_PAREN
        PERIOD
    ;

schema
    :   identifier OPEN_PAREN parameter (COMMA parameter)* CLOSE_PAREN index*
        PERIOD
    ;

index
    :   'with' spatial='spatial'? 'index' OPEN_PAREN identifier (COMMA identifier)* CLOSE_PAREN
    ;
    
fact
    :   identifier OPEN_PAREN expression (COMMA expression)* CLOSE_PAREN
        PERIOD
    ;

antifact
    :   TILDE? call
        PERIOD
    ;

function
    :   identifier OPEN_PAREN parameter? (COMMA parameter)* CLOSE_PAREN EQUALS series
        PERIOD
    ;

action
    :   identifier OPEN_PAREN parameter (COMMA parameter)* CLOSE_PAREN ARROW struct PERIOD
    ;
    
struct
    :   OPEN_BRACE
        field* (COMMA field)*
        CLOSE_BRACE
    ;
    
field
    :   literalString COLON (statement+ | series)
    ;

series
    :   expression (SEMICOLON expression)*
    ;

expression
    :   literal                                                         # literalExpression
    |   identifier                                                      # identifierExpression
    |   wildcard                                                        # wildcardExpression
    |   binding                                                         # bindingExpression
    |   parenthetical                                                   # parenExpression
    |   tuple                                                           # tupleExpression
    |   call                                                            # callExpression
    |   assign                                                          # assignExpression
    |   if                                                              # ifExpression
    |   struct                                                          # structExpression
    |   op=('-' | '!') expression                                       # unaryExpression
    |   left=expression op='::' right=expression                        # infixExpression
    |   left=expression op='^' right=expression                         # infixExpression
    |   left=expression op='~' right=expression                         # infixExpression
    |   left=expression op=('*' | '/') right=expression                 # infixExpression
    |   left=expression op=('+' | '-') right=expression                 # infixExpression
    |   left=expression op=('>' | '<' | '>=' | '<=') right=expression   # infixExpression
    |   left=expression op=('==' | '!=') right=expression               # infixExpression
    |   left=expression op=('and' | 'or' | 'xor') right=expression      # infixExpression
    |   left=expression OPEN_BRACK right=expression CLOSE_BRACK         # indexExpression
    ;

if
    :   IF condition
        (ELSE IF condition)*
        (ELSE block)?
    ;

condition
    :   OPEN_PAREN expression (SEMICOLON expression)* CLOSE_PAREN block
    ;

block
    :   OPEN_BRACE expression (SEMICOLON expression)* CLOSE_BRACE
    ;

assign
    :   identifier EQUALS expression
    ;

call
    :   many? identifier OPEN_PAREN expression? (COMMA expression)* CLOSE_PAREN using?
    ;
    
using
    :   'using' OPEN_PAREN ref=expression? (COMMA ref=expression)* CLOSE_PAREN
        'within' OPEN_PAREN threshold=expression CLOSE_PAREN
    ;

parenthetical
    :   OPEN_PAREN expression CLOSE_PAREN
    ;

tuple
    :   OPEN_BRACK expression (COMMA expression)* CLOSE_BRACK
    ;

wildcard
    :   WILDCARD
    ;

literal
    :   literalBool
    |   literalInt
    |   literalFloat
    |   literalString
    ;
    
literalBool
    :   'true' | 'false'
    ;
    
literalInt
    :   INT
    ;
literalFloat
    :   FLOAT
    ;
    
literalString
    :   STRING
    ;
  
binding
    :   AT identifier
// TODO support expressions like _ > 5 instead of @a > 5
//    |   WILDCARD
    ;

parameter
    :   identifier COLON variance? identifier
    ;

many
    :   AMP
    ;
 
variance
    :   '+' | '-'
    ;

identifier
    :   IDENTIFIER
    ;

STRING
    :   '"' (~'"')* '"'
    ;

IF
    :   'if'
    ;
    
ELSE
    :   'else'
    ;
    
IDENTIFIER
    :   [a-zA-Z] [a-zA-Z0-9_]*
    ;

INT
    :   '-'? [0-9]+
    ;

FLOAT
    :   '-'? [0-9]+ PERIOD [0-9]+
    ;

ARROW
    :   '->'
    ;

AT
    :   '@'
    ;

AMP
    :   '&'
    ;

TILDE
    :   '~'
    ;

QUESTION
    :   '?'
    ;

WILDCARD
    :   '_'
    ;

EQUALS
    :   '='
    ;

OPEN_PAREN
    :   '('
    ;

CLOSE_PAREN
    :   ')'
    ;

OPEN_BRACK
    :   '['
    ;
    
CLOSE_BRACK
    :   ']'
    ;
    
OPEN_BRACE
    :   '{'
    ;
   
CLOSE_BRACE
    :   '}'
    ;
   
PIPE
    :   '|'
    ;

COLON
    :   ':'
    ;

SEMICOLON
    :   ';'
    ;

COMMA
    :   ','
    ;

PERIOD
    :   '.'
    ;

WHITESPACE
    :   [ \t\r\n] -> channel(HIDDEN)
    ;

LINE_COMMENT
    :   '//' ~[\r\n]* -> skip
    ;