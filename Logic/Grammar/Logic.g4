grammar Logic;

logic
    :   statement+ EOF
    ;

statement
    :   type
    |   schema
    |   fact
    |   antifact
    |   function
    ;

type
    :   identifier COLON identifier (COMMA identifier)*
        PERIOD
    ;
    
schema
    :   identifier OPEN_PAREN parameter (COMMA parameter)* CLOSE_PAREN
        PERIOD
    ;
    
fact
    :   identifier OPEN_PAREN argument (COMMA argument)* CLOSE_PAREN
        PERIOD
    ;

antifact
    :   TILDE? call
        PERIOD
    ;

argument
    :   literal
    |   identifier
    ;

function
    :   identifier OPEN_PAREN parameter? (COMMA parameter)* CLOSE_PAREN EQUALS expression (SEMICOLON expression)*
        PERIOD
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
    |   op=('-' | '!') expression                                       # unaryExpression
    |   left=expression op=('+' | '-') right=expression                 # infixExpression
    |   left=expression op=('*' | '/') right=expression                 # infixExpression
    |   left=expression op=('>' | '<' | '>=' | '<=') right=expression   # infixExpression
    |   left=expression op=('==' | '!=') right=expression               # infixExpression
    |   left=expression op=('and' | 'or' | 'xor') right=expression      # infixExpression
    ;

assign
    :   identifier EQUALS expression
    ;

call
    :   identifier OPEN_PAREN expression? (COMMA expression)* CLOSE_PAREN
    ;

parenthetical
    :   OPEN_PAREN expression CLOSE_PAREN
    ;

tuple
    :   OPEN_BRACK expression (COMMA expression)+ CLOSE_BRACK
    ;

wildcard
    :   WILDCARD
    ;

literal
    :   literalBool
    |   literalInt
    |   literalFloat
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

binding
    :   AT identifier
//TODO support expressions like _ > 5 instead of a: > 5
//    |   WILDCARD
    ;

parameter
    :   identifier COLON variance? identifier
    ;
    
variance
    :   '+' | '-'
    ;

identifier
    :   IDENTIFIER
    ;

IDENTIFIER
    :   [a-zA-Z] [a-zA-Z0-9_]*
    ;

INT
    :   [0-9]+
    ;

FLOAT
    :   [0-9]+ PERIOD [0-9]+
    ;

AT
    :   '@'
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