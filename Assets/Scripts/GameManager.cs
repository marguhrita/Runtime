using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;
    public static GameManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(gameObject)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    [SerializeField] public GameObject EditorUI;

    void Update()
    {
       
    }

    // TODO make a disable movement method
    void Start()
    {
        _singleton = this;
    }

}
