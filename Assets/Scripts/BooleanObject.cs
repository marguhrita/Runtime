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



    public void Clicked()
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
        mat.EnableKeyword("_EMISSION");
    }

    public void HoverEnter() {
        float intensity = 0.03f;
        mat.SetColor("_EmissionColor", MainColour * intensity);
    }

    public void HoverExit() {
        float intensity = 0.00f;
        mat.SetColor("_EmissionColor", MainColour * intensity);
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
