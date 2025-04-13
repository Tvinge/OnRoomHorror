using AE;
using KBCore.Refs;
using System.Reflection;
using UnityEngine;
using UnityEngine.Windows;

public class AudioController : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField, Anywhere] InputReader input;
    [SerializeField, Anywhere] GroundChecker groundChecker;
    [SerializeField, Anywhere] PlayerController playerController;
    [SerializeField, Anywhere] AudioSource ambientAudio;
    [SerializeField, Anywhere] AudioSource playerFootSteps;
    [SerializeField, Anywhere] AudioClip doorOpenAudio;
    [SerializeField, Anywhere] AudioSource doorOpenAudioSource;

    [Header("Settings")]
    [SerializeField] float footStepStartPoint = 0.5f;

    private void Start()
    {
        ambientAudio.Play();
    }
    private void Update()
    {
        if (groundChecker.IsGrounded && playerController.IsMoving && !playerFootSteps.isPlaying)
        {
            PlayFootstepFromTime();
        }
        if (!playerController.IsMoving && playerFootSteps.isPlaying)
        {
            playerFootSteps.Stop();
        }
    }
    private void PlayFootstepFromTime()
    {
        playerFootSteps.time = footStepStartPoint;
        playerFootSteps.Play();
    }

    public void PlayOpeningDoorSound()
    {
        doorOpenAudioSource.PlayOneShot(doorOpenAudio);
    }

}
