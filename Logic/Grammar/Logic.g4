grammar Logic;

logic
    :   statement+ EOF
    ;

statement
    :   type
    |   schema
    |   fact
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

argument
    :   literal
    |   identifier
    ;

function
    :   identifier OPEN_PAREN parameter? (COMMA parameter)* CLOSE_PAREN EQUALS expression
        PERIOD
    ;
    
expression
    :   literal                                                         # literalExpression
    |   identifier                                                      # identifierExpression
    |   parenthetical                                                   # parenExpression
    |   call                                                            # callExpression
    |   op=('-' | '!') expression                                       # unaryExpression
    |   left=expression op=('+' | '-') right=expression                 # infixExpression
    |   left=expression op=('*' | '/') right=expression                 # infixExpression
    |   left=expression op=('and' | 'or' | 'xor') right=expression      # infixExpression
    |   left=expression op=('>' | '<' | '>=' | '<=') right=expression   # infixExpression
    |   left=expression op=('==' | '!=') right=expression               # infixExpression
//    |   expression (COMMA expression)+                                  # seriesExpression
    ;

call
    :   identifier OPEN_PAREN expression? (COMMA expression)* CLOSE_PAREN
    ;

parenthetical
    :   OPEN_PAREN expression CLOSE_PAREN
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
    
parameter
    :   identifier COLON identifier
    ;

identifier
    :   IDENTIFIER
    ;

IDENTIFIER
    :   [a-zA-Z] [a-zA-Z0-9_]*
    ;

INT
    :   [1-9] [0-9]*
    ;

FLOAT
    :   [0-9]+ PERIOD [0-9]+
    ;

QUESTION
    :   '?'
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
    
PIPE
    :   '|'
    ;

COLON
    :   ':'
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