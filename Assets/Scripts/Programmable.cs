using UnityEngine;
using UnityEngine.InputSystem;

public class Programmable : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("sigma nuts");
        
        Editor.Singleton.EditorUI.SetActive(true);
        

    }


}
