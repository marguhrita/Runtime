using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public TMP_Text codeText;
    public TMP_InputField inputField;

    public Programmable currentObject { get; private set; } // The gameobject currently being programmed
    public  bool programmingLock { get; private set; } = false;

    // Compiling stuff
    Lexer l = new();
    Parser p = new();


    void Awake()
    {
        Singleton = this;


        // GameObject codeTextObject = transform.Find("Input/TextArea/Text").gameObject;
        // if (codeTextObject != null)
        // {
        //     codeText = codeTextObject.GetComponent<TMP_Text>();
        // }
        // inputField = GetComponentInChildren<TMP_InputField>();

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
        // programmingLock = true;

        codeText.SetText(obj.Content);
        inputField.text = obj.Content;

        Debug.Log(obj.PlatformName);
        Debug.Log(obj.Content);

        codeText.SetText("Sigma Poopoo");

        // Debug.Log(codeText.text);

        Debug.Log($"Is codeText null? {codeText == null}. Is it active? {codeText.gameObject.activeInHierarchy}");


    }

    public void Submit()
    {
        List<Token> tokens = l.tokenize(codeText.text);
        List<Node> nodes = p.Parse(tokens);

        currentObject.Content = inputField.text;
        // codeText.SetText("");
        currentObject.Nodes = nodes;        // Store the nodes in the current object
        GameManager.Singleton.EditorUI.SetActive(false);


        foreach (Node n in nodes)
        {
            Debug.Log(n.ToString());
        }

    }
}
