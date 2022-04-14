using Aupl.Content;

namespace Aupl;

public class AuplVisitor : AuplBaseVisitor<object?>
{
    private Dictionary<string, object?> Variables { get; } = new();
    private int LineNumber = 0;
    
    public static Dictionary<string, int> SemanticError { get; set; } = new Dictionary<string, int>();
    public static List<object> Output { get; set; } = new List<object>();
    public AuplVisitor()
    {
        Variables["PI"] = Math.PI;
        Variables["E"] = Math.E;
        Variables["IfResult"] = null;

        Variables["Write"] = new Func<object?[], object?>(Write);
    }

    private object? Write(object?[] args)
    {
        foreach (var arg in args)
        {
            Output.Add(arg);
           // Console.WriteLine(arg);
        }

        return null;
    }

    public override object? VisitLine(AuplParser.LineContext context)
    {
        if (context.statement() != null)
        {
            LineNumber++;
            return Visit(context.statement());
        }
        else
        {
            LineNumber++;
            return Visit(context.ifBlock());
        }
    }


    public override object? VisitFunctionCall(AuplParser.FunctionCallContext context)
    {
        var name = context.IDENTIFIER().GetText();
        var args = context.expression().Select(Visit).ToArray();
        if (!Variables.ContainsKey(name))
        {
            AddSemanticError($"Function {name} is not defined", LineNumber);
        }

        if (Variables[name] is not Func<object?[], object?> func)
        {
            AddSemanticError($"Variable {name} is not defined", LineNumber);
        }
        else
        {
            return func(args);
        }
        return null;
    }

    public override object? VisitAssignment(AuplParser.AssignmentContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var value = Visit(context.expression());

        Variables[varName] = value;

        return null;
    }

    public override object? VisitIdentifierExpression(AuplParser.IdentifierExpressionContext context)
    {
        var varName = context.IDENTIFIER().GetText();

        if (!Variables.ContainsKey(varName))
        {
            AddSemanticError($"Function {varName} is not defined", LineNumber);
            return null;
        }

        return Variables[varName];
    }

    public override object? VisitConstant(AuplParser.ConstantContext context)
    {
        if (context.INTEGER() is { } i)
            return int.Parse(i.GetText());

        if (context.FLOAT() is { } f)
            return float.Parse(f.GetText());

        if (context.STRING() is { } s)
            return s.GetText()[1..^1];

        if (context.BOOL() is { } b)
            return b.GetText() == "true";

        if (context.NULL() is { })
            return null;

        AddSemanticError("Not implemented", LineNumber);
        return null;
    }

    public override object? VisitAdditiveExpression(AuplParser.AdditiveExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        var op = context.addOp().GetText();

        return op switch
        {
            "+" => Add(left, right),
            "-" => Subtract(left, right),
            _ =>  AddSemanticError("Not implemented", LineNumber),
        };
    }

    public  object? AddSemanticError(string error, int lineNumber)
    {
        if (!SemanticError.ContainsKey(error))
        {
            SemanticError.Add(error, lineNumber);
        }
        
        return null;
    }

    public override object? VisitMultiplicativeExpression(AuplParser.MultiplicativeExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        var op = context.multOp().GetText();

        return op switch
        {
            "^" => Exponent(left, right),
            "*" => Multiply(left, right),
            "/" => Divide(left, right),
            _ => AddSemanticError("Not implemented", LineNumber),
        };
    }

    public override object? VisitIfBlock(AuplParser.IfBlockContext context)
    {
        Visit(context.expression());
        var result = Variables["IfResult"];

        if (result is bool r)
            if (r)
            {
                Visit(context.block());
                return null;
            }
            else
            {
                Visit(context.elseIfBlock());
                return null;
            }


        AddSemanticError("Value cannot be compared", LineNumber);
        return null;
    }


    public override object? VisitComparisonExpression(AuplParser.ComparisonExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        var op = context.compareOp().GetText();

        return op switch
        {
            "==" => EqualTo(left, right),
            "!=" => NotEqualTo(left, right),
             ">" => GreaterThan(left, right),
             "<" => LessThan(left, right),
             ">=" => GreaterThanEqualTo(left, right),
             "<=" => LessThanEqualTo(left, right),
            _ => AddSemanticError("Not implemented", LineNumber),
        };
    }

    private object? LessThanEqualTo(object? left, object? right)
    {
        if (left is int l && right is int r)
            if (l <= r)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lf && right is float rf)
            if (lf <= rf)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is int lInt && right is float rFloat)
            if (lInt <= rFloat)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lFloat && right is int rInt)
            if (lFloat <= rInt)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }


        if (left is string lString && right is string rString)
            if (lString.Length <= rString.Length )
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }
        AddSemanticError("Not implemented", LineNumber);
        return null;
    }

    private object? GreaterThanEqualTo(object? left, object? right)
    {
        if (left is int l && right is int r)
            if (l >= r)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lf && right is float rf)
            if (lf >= rf)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is int lInt && right is float rFloat)
            if (lInt >= rFloat)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lFloat && right is int rInt)
            if (lFloat >= rInt)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }


        if (left is string lString && right is string rString)
            if (lString.Length >= rString.Length )
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }
        
        AddSemanticError("Not implemented", LineNumber);
        return null;
    }

    private object? LessThan(object? left, object? right)
    {
        if (left is int l && right is int r)
            if (l < r)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lf && right is float rf)
            if (lf < rf)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is int lInt && right is float rFloat)
            if (lInt < rFloat)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lFloat && right is int rInt)
            if (lFloat < rInt)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }


        if (left is string lString && right is string rString)
            if (lString.Length < rString.Length )
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }
        AddSemanticError("Not implemented", LineNumber);
        return null;
    }

    private object? GreaterThan(object? left, object? right)
    {
        if (left is int l && right is int r)
            if (l > r)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lf && right is float rf)
            if (lf > rf)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is int lInt && right is float rFloat)
            if (lInt > rFloat)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lFloat && right is int rInt)
            if (lFloat > rInt)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }


        if (left is string lString && right is string rString)
            if (lString.Length > rString.Length )
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        AddSemanticError("Not implemented", LineNumber);
        return null;
    }

    private object? NotEqualTo(object? left, object? right)
    {
        if (left is int l && right is int r)
            if (l != r)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lf && right is float rf)
            if (lf != rf)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is int lInt && right is float rFloat)
            if (lInt != rFloat)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lFloat && right is int rInt)
            if (lFloat != rInt)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is string && right is string)
            if (!left.Equals(right))
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        AddSemanticError("Not implemented", LineNumber);
        return null;
    }

    private object? EqualTo(object? left, object? right)
    {
        if (left is int l && right is int r)
            if (l == r)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lf && right is float rf)
            if (lf == rf)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is int lInt && right is float rFloat)
            if (lInt == rFloat)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is float lFloat && right is int rInt)
            if (lFloat == rInt)
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        if (left is string && right is string)
            if (left.Equals(right))
            {
                Variables["IfResult"] = true;
                return true;
            }
            else
            {
                Variables["IfResult"] = false;
                return false;
            }

        AddSemanticError("Not implemented", LineNumber);
        return null;
    }


    private object? Add(object? left, object? right)
    {
        if (left is int l && right is int r)
            return l + r;

        if (left is float lf && right is float rf)
            return lf + rf;

        if (left is int lInt && right is float rFloat)
            return lInt + rFloat;

        if (left is float lFloat && right is int rInt)
            return lFloat + rInt;

        if (left is string || right is string)
            return $"{left}{right}";

        AddSemanticError($"Cannot add values of types {left?.GetType()} and {right?.GetType()}.", LineNumber);
        return null;
    }

    private object? Subtract(object? left, object? right)
    {
        if (left is int l && right is int r)
            return l - r;

        if (left is float lf && right is float rf)
            return lf - rf;

        if (left is int lInt && right is float rFloat)
            return lInt - rFloat;

        if (left is float lFloat && right is int rInt)
            return lFloat - rInt;

        AddSemanticError($"Cannot subtract values of types {left?.GetType()} and {right?.GetType()}.", LineNumber);
        return null;
    }

    private object? Multiply(object? left, object? right)
    {
        if (left is int l && right is int r)
            return l * r;

        if (left is float lf && right is float rf)
            return lf * rf;

        if (left is int lInt && right is float rFloat)
            return lInt * rFloat;

        if (left is float lFloat && right is int rInt)
            return lFloat * rInt;

        AddSemanticError($"Cannot multiply values of types {left?.GetType()} and {right?.GetType()}.", LineNumber);
        return null;
    }

    private object? Exponent(object? left, object? right)
    {
        if (left is int l && right is int r)
            return Math.Pow(l, r);

        if (left is float lf && right is float rf)
            return Math.Pow(lf, rf);

        if (left is int lInt && right is float rFloat)
            return Math.Pow(lInt, rFloat);

        if (left is float lFloat && right is int rInt)
            return Math.Pow(lFloat, rInt);

        AddSemanticError($"Cannot raise values of types {left?.GetType()} and {right?.GetType()}.", LineNumber);
        return null;
    }

    private object? Divide(object? left, object? right)
    {
        if (left is int l && right is int r)
            return l / r;

        if (left is float lf && right is float rf)
            return lf / rf;

        if (left is int lInt && right is float rFloat)
            return lInt / rFloat;

        if (left is float lFloat && right is int rInt)
            return lFloat / rInt;

        AddSemanticError($"Cannot multiply values of types {left?.GetType()} and {right?.GetType()}.", LineNumber);
        return null;
    }
}
