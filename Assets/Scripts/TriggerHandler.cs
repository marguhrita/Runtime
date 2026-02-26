using UnityEngine;
using UnityEngine.Events;

public class TriggerHandler : MonoBehaviour
{
    public UnityEvent onTriggerEntered;
    private bool triggered;

    void Start()
    {
        triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            Debug.Log("Player Trighgered!");
            onTriggerEntered.Invoke();
            triggered = true;
        }
    }
   
}
