using UnityEngine;
using UnityEngine.InputSystem;


public class ProgrammableManager : MonoBehaviour
{

    [SerializeField] private LayerMask platforms;
    [SerializeField] private LayerMask boolObjs;

    PlayerInput _playerInput;
    private GameObject lastHoveredPlatform;
    private GameObject lastHoveredBool;

    private bool tutorialActive = false;
    private bool onObject = false;


    void Start()
    {
        _playerInput = GameManager.Singleton.playerInput;
        _playerInput.actions["Attack"].performed += ObjClicked;

    }

    void ObjClicked(InputAction.CallbackContext context)
    {
        if (lastHoveredPlatform != null)
        {
            Programmable p = lastHoveredPlatform.GetComponent<Programmable>();
            GameManager.Singleton.EditorUI.SetActive(true);
            Editor.Singleton.SetProgrammingObject(p);
        }
        else if (lastHoveredBool != null)
        {
            BooleanObject b = lastHoveredBool.GetComponent<BooleanObject>();
            b.Clicked();
        }
    }

    void Update()
    {
        if (!tutorialActive)
        {
            lastHoveredPlatform = Raycast(lastHoveredPlatform, platforms);
            lastHoveredBool = Raycast(lastHoveredBool, boolObjs);
        }

    }

    public void setTutorial(bool state)
    {
        tutorialActive = state;

    }

    GameObject Raycast(GameObject lastHovered, LayerMask mask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            GameObject currentHit = hit.collider.gameObject;

            if (currentHit != lastHovered)
            {
                if (lastHovered != null) OnHoverExit(lastHovered);
                OnHoverEnter(currentHit);
                lastHovered = currentHit;
            }
        }
        else if (lastHovered != null)
        {
            OnHoverExit(lastHovered);
            lastHovered = null;
        }
        return lastHovered;
    }

    void OnHoverEnter(GameObject obj)
    {
        if (obj.TryGetComponent<Programmable>(out var p))
        {
            p.HoverEnter();
        }
        else if (obj.TryGetComponent<BooleanObject>(out var b))
        {
            b.HoverExit();
        }

    }
    void OnHoverExit(GameObject obj)
    {
        if (obj.TryGetComponent<Programmable>(out var p))
        {
            p.HoverExit();
        }

        else if (obj.TryGetComponent<BooleanObject>(out var b))
        {
            b.HoverEnter();
        }
    }
}
