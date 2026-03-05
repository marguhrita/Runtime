using UnityEngine;
using System.Collections.Generic;

public class CompassBar : MonoBehaviour
{
    RectTransform compassBarTransform;
    
    [SerializeField] private Transform cameraObjectTransform;

    

    private List<RectTransform> markers = new List<RectTransform>();

    void Start(){
        compassBarTransform = GetComponent<RectTransform>();

        foreach (Transform child in transform)
    {
        Debug.Log("Found child: " + child.name);
        markers.Add(child.GetComponent<RectTransform>());
    }

    }
    
    void Update()
    {
        SetMarkerPosition(markers[0], cameraObjectTransform.position + Vector3.left * 100);
        SetMarkerPosition(markers[1], cameraObjectTransform.position + Vector3.right * 100);
        SetMarkerPosition(markers[2], cameraObjectTransform.position + Vector3.back * 100);
        SetMarkerPosition(markers[3], cameraObjectTransform.position + Vector3.forward * 100);

        
        // // For North, we project a point far away in the positive Z direction
        // SetMarkerPosition(markers, cameraObjectTransform.position + Vector3.forward * 100);
        
        // // For South, we project a point far away in the negative Z direction
        // SetMarkerPosition(southMarkerTransform, cameraObjectTransform.position + Vector3.back * 100);
    }

    private void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition) 
    {
        Vector3 directionToTarget = worldPosition - cameraObjectTransform.position;
        
        float angle = Vector2.SignedAngle(new Vector2(directionToTarget.x, directionToTarget.z), 
                                          new Vector2(cameraObjectTransform.forward.x, cameraObjectTransform.forward.z));
        
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, 0);
    }

}
