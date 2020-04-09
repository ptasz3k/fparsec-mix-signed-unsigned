namespace ClassLibrary1

module MathParser =
    open FParsec

    type public Expr =
        | Add of Expr*Expr
        | Sub of Expr*Expr
        | Val of float

    let private ws = spaces

    let private ch c = skipChar c >>. ws

    let private number: Parser<Expr, unit> = pfloat .>> ws |>> (fun x -> Val(x))

    let private opp = new OperatorPrecedenceParser<_,_,_>()

    let private expr = opp.ExpressionParser

    opp.TermParser <- choice [number]
   
    opp.AddOperator(InfixOperator("+", ws, 1, Associativity.Left, fun x y -> Add(x,y)))
    opp.AddOperator(InfixOperator("-", ws, 1, Associativity.Left, fun x y -> Sub(x,y)))

    let private completeExpression = ws >>. expr .>> eof

    let public parse s = 
        let result = run completeExpression s
        match result with
            | Success(r,_,_) -> r
            | Failure(f,_,_) -> failwith (sprintf "%A" f)

