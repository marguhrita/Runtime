using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;

public class Editor : MonoBehaviour
{
    private static Editor _singleton;
    public static Editor Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(Editor)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    public int FontSize => fontSize;
    private int lineCount = 0;

    public GameObject editor;

    [Header("Editor Options")]
    [SerializeField] private int fontSize;
    [SerializeField] private TMP_Text lineNumbers;
    [SerializeField] private TMP_Text codeText;


    void Start()
    {
        Singleton = this;

        // Match size of line numbers and codetext
        lineNumbers.fontSize = fontSize;
        //lineNumbers.text = "1";
        codeText.fontSize = fontSize;

        editor = gameObject;
    }

    public void OnValueChanged(string text)
    {
        if (text.EndsWith("\n"))
        {
            Debug.Log("Newline detected!!");
            if (Input.GetKeyDown(KeyCode.Return))
            {
                lineNumbers.text += "\n" + lineCount;
                lineCount += 1;
            }
        }

    }
    
    public void submit()
    {
        Lexer l = new Lexer();
        List<Token> tokens = l.tokenize(codeText.text);
        foreach (Token t in tokens)
        {
            Debug.Log(t.ToString());
        }


        // Parse the token list
        

        gameObject.SetActive(false);
    }
}
