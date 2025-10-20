using UnityEngine;
using TMPro;


public class TextInputHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField textArea;
    [SerializeField] RectTransform lineNumbers;

    void Update()
    {
        Debug.Log("Viewport: " + textArea.textViewport.position);
        //Debug.Log("Anchored: " + textArea.);

    }
}
