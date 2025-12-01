using System.Collections.Generic;
using UnityEngine;
using AppleCore.Node;
public class Programmable : MonoBehaviour
{
    public List<Node> Nodes { get; set; }
    public string Content{ get; set; }
    void OnMouseDown()
    {
        GameManager.Singleton.EditorUI.SetActive(true);
        Editor.Singleton.SetProgrammingObject(this);
    }

}
