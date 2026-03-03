using UnityEngine;

public class ProgrammableManager : MonoBehaviour
{

    [SerializeField] private LayerMask interactableLayer; 
    private GameObject lastHovered;
    void Update()
    {
        ProgrammableObjectRaycast();
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
        p.HoverEnter();
    }
    void OnHoverExit(GameObject obj)
    {
        Programmable p = obj.GetComponent<Programmable>();
        p.HoverExit();
    }
}
