using UnityEngine;
using Dossamer.Dialogue.Schema;
using Dossamer.Dialogue;
using UnityEngine.InputSystem;
using System.Data;
using UnityEngine.UI;
using Unity.Cinemachine;
using System;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Cutscene[] cutscenes;
    private PlayerInput _playerInput;    
    [SerializeField] private RawImage progressionIndicator;
    [SerializeField] private float delay;
    
    [SerializeField] private int[] cameraSteps;

    [Header("Cameras")]
    public CinemachineCamera playerCamera;
    public CinemachineCamera[] panTargetCamera; 
    
    private int cameraCount;
    private int currentStep;
    [SerializeField] private float cameraPanSpeed;

    private bool isPanning = false;
    private CinemachineCamera activePanCamera;

    private TutorialAudioManager tutorialAudioManager;

    public void TriggerEntered(int CutsceneNumber)
    {
        // Debug.Log(CutsceneNumber - 1);
        DialogueManager.Instance.StartNewDialogue(cutscenes[CutsceneNumber - 1]);
        _playerInput = GameManager.Singleton.playerInput;
        _playerInput.actions["Attack"].performed += DialogueManager.Instance.ProgressDialogueEvent;
        progressionIndicator.enabled = false;

        tutorialAudioManager.PlayClip(CutsceneNumber-1);
    }

    void Start()
    {
        tutorialAudioManager = GetComponent<TutorialAudioManager>();
        DialogueManager.Instance.TextPanel.OnTextDoneIterating += StepDone;
        currentStep = 0;
        cameraCount = 0;
    }

    void Update()
    {
        if (isPanning && activePanCamera != null)
        {
            var orbitalFollow = activePanCamera.GetComponent<CinemachineOrbitalFollow>();
            if (orbitalFollow != null)
            {
                orbitalFollow.HorizontalAxis.Value += Time.deltaTime * cameraPanSpeed;
            }
        }
    }

    public void NextTutorialStep()
    {
        progressionIndicator.enabled = false;
        currentStep += 1;
        DialogueManager.Instance.SetIsProgressionFrozen(true);

        if (cameraCount < cameraSteps.Length && currentStep == cameraSteps[cameraCount])
        {
            // Activate the corresponding camera
            activePanCamera = panTargetCamera[cameraCount];
            activePanCamera.Priority = 15;

            // Start the panning in the Update loop
            isPanning = true;

            // Move to the next camera in the list for the next time
            cameraCount++;
        }
    }

    public IEnumerator showIndicator()
    {
        yield return new WaitForSeconds(delay);
        progressionIndicator.enabled = true;

        // Stop the camera from spinning once the text finishes
        isPanning = false; 

        // Lower the priority of the camera we just used so it blends back to the player
        if (cameraCount > 0 && activePanCamera != null)
        {
            activePanCamera.Priority = 0;
            activePanCamera = null;
        }

    }

    // public void DialogueStarted()
    // {
    //      // play the first clip
    // }

    void StepDone()
    {
        StartCoroutine(showIndicator());
        DialogueManager.Instance.SetIsProgressionFrozen(false);

    }
}