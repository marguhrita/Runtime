using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;
using AppleCore.Node;

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
    private TMP_Text lineNumbers;
    private TMP_Text codeText;

    public Programmable currentObject { get; private set; } // The gameobject currently being programmed
    public  bool programmingLock { get; private set; } = false;

    // Compiling stuff
    Lexer l = new();
    Parser p = new();


    void Awake()
    {
        Singleton = this;


        GameObject codeTextObject = transform.Find("Input/TextArea/Text").gameObject;
        if (codeTextObject != null)
        {
            codeText = codeTextObject.GetComponent<TMP_Text>();
        }

        codeText.fontSize = fontSize;

        // EditorUI.SetActive(false);

    }

    public void OnValueChanged(string text)
    { 
        if (text.EndsWith("\n"))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                lineNumbers.text += "\n" + lineCount;
                lineCount += 1;
            }
        }
    }

    public void SetProgrammingObject(Programmable obj)
    {
        currentObject = obj;
        programmingLock = true;
    }

    public void Submit()
    {
        List<Token> tokens = l.tokenize(codeText.text);
        List<Node> nodes = p.Parse(tokens);

        gameObject.SetActive(false);
        currentObject.Content = codeText.text;
        currentObject.Nodes = nodes;        // Store the nodes in the current object

        foreach (Node n in nodes)
        {
            Debug.Log(n.ToString());
        }

    }
}
