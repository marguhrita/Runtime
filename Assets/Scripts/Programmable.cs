using System.Collections.Generic;
using UnityEngine;
using AppleCore.Node;
using DG.Tweening;
using System;
using System.IO.Compression;
using UnityEngine.InputSystem;
public class Programmable : MonoBehaviour
{
    public List<Node> Nodes { get; set; } = new List<Node>();
    [TextArea(4,10)]
    [SerializeField] public string Content;
    [SerializeField] public bool Editable;

    private bool running;
    public float duration;
    public String PlatformName = "";

    // Shader Colours
    private Material platformMat;
    private int colourID;
    private Color lastColour;
    private bool mouseOver;
    private bool recursing;
    private bool finished;

    void Start()
    {
        running = false;
        // Debug.Log(gameObject.name + " script is running");
        
        // Shader Colours
        platformMat = gameObject.GetComponent<Renderer>().material;
        colourID = Shader.PropertyToID("_FresnelColor");
        lastColour = ChooseTargetColour();
        platformMat.SetColor(colourID, lastColour); // init colour


    }

    void Update()
    {
        // Change colour of the platform if state changed
        Color nextColour = ChooseTargetColour();
        if (lastColour != nextColour)
        {
            lastColour = nextColour;

            platformMat.SetColor(colourID, nextColour);

        }
    }

    // void OnMouseDown()
    // {
    //     GameManager.Singleton.EditorUI.SetActive(true);
    //     Editor.Singleton.SetProgrammingObject(this);
    // }

    private void lightUp(bool state)
    {
        if (state)
        {
            platformMat.SetFloat("_FresnelNess", 200f);
            platformMat.SetFloat("_GlowBrightness", 7f);
        }
        else { 
        platformMat.SetFloat("_FresnelNess", 0f);
        platformMat.SetFloat("_GlowBrightness", 0f);

        }
    }

    public void HoverEnter()
    {
        mouseOver = true;
        lightUp(true);
    }
        
    public void HoverExit()
    {
        mouseOver = false;
        lightUp(false);

    }

   

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !running && !finished) // debounce
        {
            collision.transform.SetParent(transform); // pretty sure this was to keep the fish on the platform
            StartCoroutine(Run(Nodes));
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("PLAYER DETECTED out!!!");
            collision.transform.SetParent(null);

        }
    }

    System.Collections.IEnumerator Run(List<Node> nodes)
    {
        if (nodes == null || nodes.Count == 0)
        {
            Debug.Log("No nodes found");
            yield break;
        }


        Debug.Log("Running...");
        foreach (Node n in nodes)
        {
            running = true;
            Debug.Log(n.ToString());
            switch (n)
            {
                case Call c:
                    // Debug.Log("Call detecc");
                    Debug.Log(c.identifier);
                    switch (c)
                    {
                        case var x when x.identifier == "move":
                            Debug.Log("Move");
                            break;
                        case var x when x.identifier == "movex" || x.identifier == "movey" || x.identifier == "movez":
                            if (x.args.Count != 1) // 1 argument checker
                            {
                                Debug.LogError("Specific Move statement should only have one arg");
                            }

                            // Definitely a better way to do this somewhere
                            if (x.args[0] is IntLit val)
                            {
                                if (x.identifier.EndsWith("x"))
                                {
                                    Debug.Log("X");
                                    Debug.Log(val.value);
                                    MoveObject((float)val.value, 0f, 0f);
                                }
                                else if (x.identifier.EndsWith("y"))
                                {
                                    MoveObject(0f, (float)val.value, 0f);
                                }
                                else if (x.identifier.EndsWith("z"))
                                {
                                    MoveObject(0f, 0f, (float)val.value);
                                }


                                StartCoroutine(RunTimerReset());
                                yield return new WaitUntil(() => !running);

                            }

                            break;


                    }

                    break;
                case IfStmt i:
                    // Debug.Log("IF Statement Detected");
                    switch (i)
                    {
                        case var x when x.condition is Var:
                            Debug.Log("Variable in the condition!");
                            if (x.condition is Var v)
                            {
                                bool cond = GameManager.Singleton.BoolDict[v.identifier];
                                Debug.Log("Got condition!: " + cond.ToString());
                                if (cond)
                                {
                                    Debug.Log(i.body[0]);
                                    recursing = true;
                                    yield return StartCoroutine(Run(i.body)); // runs the body of the IF statment

                                }
                                running = false;
                            }
                            break;
                    }

                    break;
            }
        }
        
    }


    System.Collections.IEnumerator RunTimerReset()
    {
        yield return new WaitForSeconds(duration);
        running = false;
        // Debug.Log("TImer REset!");
    }

    void MoveObject(float x, float y, float z)
    {
        // Debug.Log("Moving");
        try
        {
            Vector3 target = transform.position + new Vector3(x, y, z);
            transform.DOMove(target, duration);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error: {e.Message}");
        }
        
    }

    private Color ChooseTargetColour()
    {
        if (mouseOver) return Color.green;
        if (Nodes.Count == 0) return Color.gray;
        if (running)
        {
            lightUp(true);
            return Color.rebeccaPurple;
        }
        lightUp(false);
        return Color.aquamarine;

    }

}
