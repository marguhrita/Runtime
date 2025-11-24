using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using AppleCore.Node;
public class Programmable : MonoBehaviour
{
    public List<Node> Nodes { get; set; }
    void OnMouseDown()
    {
        Editor.Singleton.EditorUI.SetActive(true);
    }


}
