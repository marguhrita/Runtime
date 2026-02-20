using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AppleCore.Node;
using Unity.VisualScripting;
using UnityEngine;

public class Parser
{
    private List<Token> tokens;
    public List<Node> Parse(List<Token> tokens)
    {
        // Init tokens
        this.tokens = tokens;

        List<Node> stmts = Parse_stmts();
        List<Node> defs = new List<Node>();

        List<Node> combinedList = defs.Concat(stmts).ToList();
        return combinedList;
    }

    // Check if next token is a specific type
    private bool check(TokenType type)
    {
        return peek().type == type;
    }

    // Give the next token in the list
    private Token peek()
    {
        return tokens[0];
    }

    private Token match()
    {
        Token t = tokens[0];
        tokens.RemoveAt(0);
        return t;
    }

    private bool is_stmt_next()
    {
        return is_expr_next() |
        check(TokenType.IF);
    }

    private bool is_expr_next()
    {
        return check(TokenType.ID);
    }

    private List<Node> Parse_stmts()
    {
        List<Node> stmts = new List<Node>();
        while (is_stmt_next())
        {
            Node node = Parse_stmt();
            stmts.Add(node);
        }
        return stmts;
    }
    private Node Parse_stmt()
    {
        // IF statements and whatnot
        // if (Boolean Condition){
        //     Body
        // }
        if (check(TokenType.IF))
        {
            match();
            if (check(TokenType.LPAR))
            { //(
                match();
                Node expr = Parse_expr();
                _ = new List<Node>();
                List<Node> stmts = Parse_stmts();
                return new IfStmt(expr, stmts);
            }
        }
        return Parse_assign_and_expr();
    }
    private Node Parse_assign_and_expr()
    {
        // Do assigns at some point

        return Parse_Unary();
    }

    private Node Parse_Unary()
    {

        if (check(TokenType.NEGATE))
        {
            match(); // match negate
            Node primary_expr = Parse_primary_expr();
            if (primary_expr is IntLit intLit) // make sure it is a number for the negate operator
            {
                return new IntLit(-intLit.value);
            }
        }
        else
        {
            return Parse_primary_expr();
        }

        throw new Exception("Something went wrong parsing Unary...");
    }

    private Node Parse_primary_expr()
    {
        // Numbers
        if (check(TokenType.NUMBER))
        {
            Token t = match();
            return new IntLit(int.Parse(t.value.ToString()));
        }

        // Booleans
        if (check(TokenType.BOOLEAN))
        {
            Token t = match();
            return new BoolLit(t.value.ToString() == "True"); // Should only be True or False, so this is fine 💀
        }

        return Parse_expr();

    }


    // Needs to be sure it is a call here
    private Node Parse_expr()
    {
        if (check(TokenType.ID)) // identifiers
        {
            string id_name = (string)match().value;
            if (check(TokenType.LPAR)) // Function call
            {
                match(); // Left Bracket
                List<Node> args = Parse_args();

                match(); // Right Bracket
                return new Call(id_name, args);
            }
            else // Should be a variable
            {
                match();
                return new Var(id_name);
            }
        }
        else
        {
            Token t = peek();
            throw new Exception($"Expected Function Call. got {t.type}");
        }

        return null; // This should never happen (please)
    }

    private List<Node> Parse_args()
    {
        // Add the first arg, and then get repetitions
        List<Node> args = new List<Node>
        {
            Parse_Unary()
        };
        args.Concat(Parse_argrep());
        return args;
    }

    private List<Node> Parse_argrep()
    {
        if (check(TokenType.COMMA))
        {
            match();
            Node expr = Parse_Unary();
            List<Node> argrep = Parse_argrep(); // recursion my beloved
            return new List<Node>() { expr }.Concat(argrep).ToList();
        }

        return new List<Node>(); // null case - no more args
    }



}




