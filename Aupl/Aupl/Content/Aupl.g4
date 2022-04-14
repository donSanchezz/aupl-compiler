grammar Aupl;
/*
 * Parser Rules
 */
program: line*  EOF;

line: statement | ifBlock ;

statement: (assignment|functionCall) ';';

ifBlock: 'if' expression block ('else' elseIfBlock);

elseIfBlock: block | ifBlock;

assignment: IDENTIFIER '=' expression;

functionCall : IDENTIFIER '(' (expression (',' expression)*)? ')';

expression
    : constant                                          #constantExpression
    | IDENTIFIER                                        #identifierExpression
    | functionCall                                      #functionalCallExpression
    | '(' expression ')'                                #parenthesizedExpression
    | '!' expression                                    #notExpression
    | expression multOp expression                      #multiplicativeExpression
    | expression addOp expression                       #additiveExpression
    | expression compareOp expression                   #comparisonExpression
    | expression boolOp expression                      #booleanExpression
    ;

multOp: '^'| '*' | '/' | '%' ; 
addOp: '+' | '-' ;
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=' ;
boolOp: BOOL_OPERATOR;    
block: '{' line* '}' ;
/*
 * Lexer Rules
 */
BOOL_OPERATOR: 'and' | 'or';

constant: INTEGER | FLOAT | STRING | BOOL | NULL;

INTEGER: [0-9]+;
FLOAT: [0-9]+ '.' [0-9]+;
STRING: ('"' ~'"'* '"') | ('\'' ~'\''* '\''); //Quote followed by any number of non quotes
BOOL: 'true' | 'false';
NULL: 'null';




WS: [ \t\r\n]+ -> skip;
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;
   