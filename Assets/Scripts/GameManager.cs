using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    }

}
