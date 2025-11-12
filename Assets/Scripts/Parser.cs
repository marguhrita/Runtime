using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEditor.UI;
using UnityEngine.DedicatedServer;

public class Parser
{
    private List<Token> tokens;
    public List<Node> Parse()
    {
        List<Node> stmts = new List<Node>();
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
        if (check(TokenType.ASSIGN))
        {
            return parse_assitn();
        }

    }

    private Node Parse_assign_and_expr()
    {



    }

    private Node Parse_primary_expr()
    {
        if (check(TokenType.NUMBER))
        {
            Token t = match();
            return new IntVar();
        }

    }

    private Node Parse_call_expr()
    {
        if (check(TokenType.ID))
        {
            string id_name = (string)match().value;
            if (check(TokenType.LPAR))
            {
                match();
                List<Node> args = Parse_args();
            }
        }
    }

    private List<Node> Parse_args()
    {
        List<Node> args = new List<Node>();
        return args;
    }

    private List<Node> Parse_argrep()
    {
        if (check(TokenType.COMMA))
        {
            match();
            Node expr = Parse_primary_expr();
            List<Node> argrep = Parse_argrep();
            return new List<Node>() { expr }.Concat(argrep).ToList();
        }

        return new List<Node>(); // null case - no more args
    }


 
}




