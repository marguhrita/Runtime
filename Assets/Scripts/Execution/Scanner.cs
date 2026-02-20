using System;
using System.Text;
using NUnit.Framework.Constraints;
using UnityEngine.InputSystem.Interactions;
using UnityEngine;


public class Scanner
{
    private StringBuilder Buf;
    public string Alphanumeric { get; private set; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public Scanner(string code)
    {
        Buf = new StringBuilder(code);
    }

    public char? Peek()
    {
        // Check if string is empty
        if (Buf.Length <= 1)
        {
            return null;
        }

        // Return next character
        return Buf[0];
    }

    public void Consume(int n = 1)
    {
        // consumes the first n letters in the buffer, 1 by default
        Buf.Remove(0, n);
    }

    public string peekWord()
    {
        StringBuilder s = new StringBuilder("");

        
        char c = (char)Peek();
        while (Alphanumeric.Contains(c))
        {
            s.Append(c);
            Consume();

            if (Peek() is not char)
            {
                break;
            }
            c = (char)Peek();
        }

        return s.ToString();
    }

    public string peekString()
    {
        StringBuilder s = new StringBuilder("");

        
        char c = (char)Peek();
        while (Alphanumeric.Contains(c) || c == ' ')
        {
            s.Append(c);
            Consume();

            if (Peek() is not char)
            {
                break;
            }
            c = (char)Peek();
        }

        return s.ToString();
    }

    public float peekFloat()
    {
        StringBuilder s = new StringBuilder("");

        char c = (char)Peek();

        while (char.IsDigit(c))
        {
            s.Append(c);
            Consume();

            c = (char)Peek();
        }

        float result;
        float.TryParse(s.ToString(), out result);
        return result;
    }
    }
