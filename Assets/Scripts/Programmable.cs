using System.Collections.Generic;
using UnityEngine;
using AppleCore.Node;
public class Programmable : MonoBehaviour
{
    public List<Node> Nodes { get; set; }
    public string Content { get; set; }
    private bool running;

    void Start()
    {
        running = false;
    }

    void OnMouseDown()
    {
        GameManager.Singleton.EditorUI.SetActive(true);
        Editor.Singleton.SetProgrammingObject(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER DETECTED!!!");
            running = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER DETECTED!!!");
            running = false;
        }
    }

    void Run() {
        if (Nodes.Count == 0)
        {
            Debug.Log("No nodes found");
        }
    }

    void MoveObject()
    {
        
    }

}
