using UnityEngine;
using UnityEngine.InputSystem;


public class ProgrammableManager : MonoBehaviour
{

    [SerializeField] private LayerMask interactableLayer; 
    [SerializeField] PlayerInput _playerInput;   
    private GameObject lastHovered;
    private bool tutorialActive = false;
    private bool onObject = false;
    

    void Start()
    {
        _playerInput.actions["Attack"].performed += ProgrammableClicked;
    }

    void ProgrammableClicked(InputAction.CallbackContext context) {
        if (lastHovered != null)
        {
            Programmable p = lastHovered.GetComponent<Programmable>();
            GameManager.Singleton.EditorUI.SetActive(true);
            Editor.Singleton.SetProgrammingObject(p);
        }
    }

    void Update()
    {
        if (!tutorialActive)
        {
            ProgrammableObjectRaycast();
        }
        
    }

    public void setTutorial(bool state)
    {
        tutorialActive = state;
        
    }

    void ProgrammableObjectRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
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
    }

    void OnHoverEnter(GameObject obj)
    {
        Programmable p = obj.GetComponent<Programmable>();
        onObject = true;
        p.HoverEnter();
    }
    void OnHoverExit(GameObject obj)
    {
        Programmable p = obj.GetComponent<Programmable>();
        onObject = false;
        p.HoverExit();
    }
}
