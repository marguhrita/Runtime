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

    void Run()
    {
        if (Nodes.Count == 0)
        {
            Debug.Log("No nodes found");
        }

        foreach (Node n in Nodes)
        {
            switch (n)
            {
                case Call c:
                    switch (c)
                    {
                        case var x when x.identifier == "Move":
                            Debug.Log("Move");
                            break;
                        case var x when x.identifier == "MoveX" || x.identifier == "MoveY" || x.identifier == "MoveZ":
                            if (x.args.Count != 1) // 1 argument checker
                            {
                                Debug.LogError("Specific Move statement should only have one arg");
                            }

                            // Definitely a better way to do this somewhere
                            if (x.args[0] is IntLit val)
                            {
                                if (x.identifier.EndsWith("X"))
                                {

                                    MoveObject((float)val.value, 0f, 0f);
                                }
                                else if (x.identifier.EndsWith("Y"))
                                {
                                    MoveObject(0f, (float)val.value, 0f);
                                }
                                else if (x.identifier.EndsWith("Z"))
                                {
                                    MoveObject(0f, 0f, (float)val.value);
                                }
                            }
                                
                            break;
                            
                    }
                    break;
            }
        }

    }

    void MoveObject(float x, float y, float z)
    {
        
    }

}
