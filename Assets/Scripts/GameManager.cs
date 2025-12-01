using Unity.VisualScripting;
using UnityEngine;

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



    // TODO make a disable movement method
    void Start()
    {
        _singleton = this;
    }

}
