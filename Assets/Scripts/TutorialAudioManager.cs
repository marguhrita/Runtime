using System.Collections;
using UnityEngine;

public class TutorialAudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] Level1Clips;
    [SerializeField] AudioClip[] Level2Clips;
    [SerializeField] AudioClip[] Level3Clips;
    [SerializeField] AudioClip[] Level4Clips;
    public enum Levels {Level1, Level2, Level3, Level4}
    public Levels currentLevel;
    private AudioClip[] currentClip;
    [SerializeField] private AudioSource _audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.spatialBlend = 0; // makes audio global
        switch (currentLevel)
        {
            case Levels.Level1:
                currentClip = Level1Clips;
                break;
            case Levels.Level2:
                currentClip = Level2Clips;
                break;
            case Levels.Level3:
                currentClip = Level3Clips;
                break;
            case Levels.Level4:
                currentClip = Level4Clips;
                break;
        }
    }

    public void PlayClip(int step)
    {
        _audioSource.clip = currentClip[step];
        _audioSource.Play();

    }

}
