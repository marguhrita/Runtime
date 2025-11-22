using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

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

    [field: SerializeField] public GameObject EditorUI { get; private set; }

    [Header("Editor Options")]
    [SerializeField] private int fontSize;
    private TMP_Text lineNumbers;
    private TMP_Text codeText;


    void Awake()
    {
        Singleton = this;

        // Init editor vars
        EditorUI = gameObject;

        GameObject codeTextObject = transform.Find("Input/TextArea/Text").gameObject;
        if (codeTextObject != null)
        {
            codeText = codeTextObject.GetComponent<TMP_Text>();
        }


        // Match size of line numbers and codetext
        //lineNumbers.fontSize = fontSize;
        //lineNumbers.text = "1";
        codeText.fontSize = fontSize;

        EditorUI.SetActive(false);

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
        // foreach (Token t in tokens)
        // {
        //     Debug.Log(t.ToString());
        // }


        // Parse the token list
        Parser p = new Parser();
        List<Node> nodes = p.Parse(tokens);
        Debug.Log(nodes);
        foreach (Node n in nodes)
        {
            Debug.Log(n.ToString());
        }

        gameObject.SetActive(false);
    }
}
