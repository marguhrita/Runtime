using TMPro;
using Unity.VisualScripting;
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

public class Tokenizer
{
    private string buf;

    public void tokenize(string code)
    {
        foreach (char c in code){
            buf += c; // add new character to character buffer

            // Checks for things
            if (buf.ToLower() == "variable")
            {
                
            }
        }
    }
}
