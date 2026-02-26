using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] public GameObject Player;
    [SerializeField] public BooleanObject[] BooleanGameObjects;
    public Dictionary<string, bool> BoolDict;


    void Update()
    {
        if (Player.transform.position.y <= -100)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // TODO make a disable movement method
    void Start()
    {
        _singleton = this;
        if (BooleanGameObjects.Length != 0){
        SerializeBoolDict();

        }
    }

    public void SerializeBoolDict()
    {
        BoolDict = new Dictionary<string, bool>();
        foreach (BooleanObject b in BooleanGameObjects)
        {
            BoolDict[b.Name] = b.Value;
        }
    }

}
