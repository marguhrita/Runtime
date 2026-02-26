using UnityEngine;
using Dossamer.Dialogue.Schema;
using Dossamer.Dialogue;


public class TutorialManager : MonoBehaviour
{
    [SerializeField] Cutscene cutscene;

    void Start()
    {
        // DialogueManager.Instance.StartNewDialogue(cutscene);
    }

    public void FirstTriggerEntered()
    {
        DialogueManager.Instance.StartNewDialogue(cutscene);
    }
}
