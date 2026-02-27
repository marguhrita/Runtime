using UnityEngine;
using Dossamer.Dialogue.Schema;
using Dossamer.Dialogue;
using UnityEngine.InputSystem;
using System.Data;
using UnityEngine.UI;
using Unity.Cinemachine;
using System;
using Unity.VisualScripting;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Cutscene[] cutscenes;
    [SerializeField] PlayerInput _playerInput;    
    [SerializeField] private RawImage progressionIndicator;
    [SerializeField] private float delay;

    [Header("Cameras")]
    public CinemachineCamera playerCamera;
    public CinemachineCamera panTargetCamera;
    private int currentStep;
    [SerializeField] private float cameraPanSpeed;



    public void TriggerEntered(int CutsceneNumber)
    {
        DialogueManager.Instance.StartNewDialogue(cutscenes[CutsceneNumber - 1]);
        _playerInput.actions["Attack"].performed += DialogueManager.Instance.ProgressDialogueEvent;
        progressionIndicator.enabled = false;
    }

    void Start()
    {
        DialogueManager.Instance.TextPanel.OnTextDoneIterating += StepDone;
        currentStep = 0;
    }

    public void NextTutorialStep()
    {
        progressionIndicator.enabled = false;
        currentStep += 1;
    }

    public IEnumerator showIndicator()
    {
        yield return new WaitForSeconds(delay);
        progressionIndicator.enabled = true;
    }

    void Update()
    {
        if (currentStep == 2)
        {
            panTargetCamera.Priority = 15;
            panCamera(panTargetCamera);
        }
    }

    void panCamera(CinemachineCamera cam)
    {
        cam.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value += Time.deltaTime * cameraPanSpeed;
    }

    void StepDone()
    {
        panTargetCamera.Priority = 0;
        StartCoroutine(showIndicator());
    }

}
