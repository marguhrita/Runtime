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

    }



    public void OnValueChanged(string text)
    {
        if (text.EndsWith("\n"))
        {
            Debug.Log("Newline detected!!");
            if (Input.GetKeyDown(KeyCode.Return)){
                lineNumbers.text += "\n" + lineCount;
                Debug.Log("Added line " + lineCount);
                lineCount += 1;
            }
        }
        
    }
}

public class Parsenizer
{
    private string buf;
    private List<Node> nodes;
    public void tokenize(string code)
    {
        int i = 0;

        while (i <= code.Length)
        {
            buf += code[i]; // add new character to character buffer

            // Checks for things
            // Variable check
            if (buf.ToLower() == "variable")
            {
                string id = findIdentifier(code[(i + 1)..]);
                
                buf = "";
            }


            // Function call check
            else
            {
                string id = findIdentifier(code[i..]);
            }


        }
    }
    
    private string findIdentifier(string code)
    {
        int i = 0;
        string identifier = "";
        while (code[i] != ' ') // loop until a space is found 
        {
            identifier += code[i];
            i += 1;
        }

        return identifier;
    }
}
