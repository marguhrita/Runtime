using UnityEngine;
using System;

[Serializable]
public class BooleanObject : MonoBehaviour
{
    [SerializeField] public string Name;
    [SerializeField] private Color MainColour;
    [SerializeField] public bool Value;
    private Material mat;
    private Color lastColour;
    private int colourID;



    void OnMouseDown()
    {
        Value = !Value;
        GameManager.Singleton.BoolDict[Name] = Value;
    }

    void Start()
    {
        mat = GetComponent<Renderer>().material;

        colourID = Shader.PropertyToID("_BaseColor");
        lastColour = ChooseTargetColour();
        mat.SetColor(colourID, lastColour);
    }

    void Update()
    {
        // Change colour of the platform if state changed
        Color nextColour = ChooseTargetColour();
        if (lastColour != nextColour)
        {
            lastColour = nextColour;

            mat.SetColor(colourID, nextColour);

        }
    }
    
    private Color ChooseTargetColour()
    {
        if (Value)
        {
            return MainColour;
        }
        else
        {
            return Color.gray1; // inactive

        }

    }
}
