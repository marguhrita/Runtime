using UnityEngine;
using System;

[Serializable]
public class BooleanObject : MonoBehaviour
{
    [SerializeField] public string Name;
    [SerializeField] public bool Value;

    void OnMouseDown()
    {
        Value = !Value;
        GameManager.Singleton.BoolDict[Name] = Value;
    }
    
    private Color ChooseTargetColour()
    {
        if (Value)
        {
            return Color.green;
        }
        else
        {
            return Color.red;

        }

    }
}
