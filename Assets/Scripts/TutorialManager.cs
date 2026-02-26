using UnityEngine;
using Dossamer.Dialogue.Schema;
using Dossamer.Dialogue;
using UnityEngine.InputSystem;
using System.Data;
using UnityEngine.UI;

using System;
using Unity.VisualScripting;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Cutscene cutscene;
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] String[] TutorialSteps;
    [SerializeField] private RawImage progressionIndicator;


    public void FirstTriggerEntered()
    {
        DialogueManager.Instance.StartNewDialogue(cutscene);
        _playerInput.actions["Attack"].performed += DialogueManager.Instance.ProgressDialogueEvent;
        _playerInput.actions["Attack"].performed += DialogueManager.Instance.ProgressDialogueEvent;

        progressionIndicator.enabled = false;

    }

    void Start()
    {
        DialogueManager.Instance.TextPanel.OnTextDoneIterating += StepDone;
    }

    public void NextTutorialStep(float delay)
    {
        StartCoroutine(showIndicator(delay));
    }

    public IEnumerator showIndicator(float delay)
    {
        yield return new WaitForSeconds(delay);
        progressionIndicator.enabled = false;
    }

    void StepDone()
    {
        progressionIndicator.enabled = true;
    }

}
