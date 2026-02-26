using UnityEngine;
using Dossamer.Dialogue.Schema;
using Dossamer.Dialogue;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Cutscene cutscene;
    [SerializeField] PlayerInput _playerInput;

    public void FirstTriggerEntered()
    {
        DialogueManager.Instance.StartNewDialogue(cutscene);
        _playerInput.actions["Attack"].performed += DialogueManager.Instance.ProgressDialogueEvent;
    }

}
