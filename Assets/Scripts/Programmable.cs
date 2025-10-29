using UnityEngine;
using UnityEngine.InputSystem;

public class Programmable : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("sigma nuts");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("huh");

        }
        Editor.Singleton.editor.SetActive(true);

    }


}
