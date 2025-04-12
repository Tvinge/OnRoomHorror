using AE;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Windows;

public class AudioController : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField, Anywhere] InputReader input;
    [SerializeField, Anywhere] GroundChecker groundChecker;
    [SerializeField, Anywhere] PlayerController playerController;
    [SerializeField, Anywhere] AudioSource ambientAudio;
    AudioSource playerFootSteps;


    [Header("Settings")]
    [SerializeField] float footStepStartPoint = 0.5f;



    private void Awake()
    {
        //input.Move += OnMove;

    }
    private void Start()
    {
        playerFootSteps = playerController.GetComponent<AudioSource>(); 
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


    void OnMove(Vector2 a) //needed to be called twice 2 work
    {
        if (playerController.IsMoving && !playerFootSteps.isPlaying)
        {
            playerFootSteps.Play();
        }
        else if (playerFootSteps.isPlaying)
        {
            playerFootSteps.Stop();
        }
    }
}
