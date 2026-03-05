using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.InputSystem;


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
    public PlayerInput playerInput { get; private set; }


    void Update()
    {
        if (Player.transform.position.y <= -100)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Awake()
    {
        playerInput = Player.GetComponent<PlayerInput>();
        _singleton = this;
        BoolDict = new Dictionary<string, bool>();      
    }


}
