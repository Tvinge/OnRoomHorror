using Unity.Cinemachine;
using UnityEngine;
using UnityEditor;
using KBCore.Refs;
using Cysharp.Threading.Tasks.Triggers;

namespace AE
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Self] GroundChecker groundChecker;
        [SerializeField, Self] CharacterController controller;
        [SerializeField, Self] Transform playerTransform;
        [SerializeField, Anywhere] CinemachineCamera freeLookVCam;
        [SerializeField, Anywhere] InputReader input;
        [SerializeField, Anywhere] RayCaster rayCaster;
        //s
        [Header("Settings")]
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;
        [SerializeField] float minMovementSpeedDetection = 0.1f;
        [SerializeField] public bool hasLight = false;

        const float ZeroF = 0f;

        float currentSpeed;
        float velocity;

        public bool IsMoving => currentSpeed > minMovementSpeedDetection;

        Transform mainCamera;
        private void Awake()
        {
            
            mainCamera = Camera.main.transform;
            freeLookVCam.Follow = transform;
            freeLookVCam.LookAt = transform;
            freeLookVCam.OnTargetObjectWarped(transform, positionDelta: transform.position - freeLookVCam.transform.position - Vector3.forward);

        }

        private void Start()
        {
            input.EnablePlayerActions();
        }
        private void Update()
        {
            HandleMovement();
            HandleGravity();
        }

        void HandleGravity()
        {
            if (!groundChecker.IsGrounded && Time.time > 5) //bandaid to make sure that the collider on the player loads before the ground checker
                transform.position += Vector3.up * Physics.gravity.y * Time.deltaTime;
        }

        void HandleMovement()
        {
            var movementDirection = new Vector3(input.Direction.x, 0, input.Direction.y).normalized;
            var adjustedDirection = Quaternion.AngleAxis(mainCamera.rotation.eulerAngles.y, Vector3.up) * movementDirection;

            if (adjustedDirection.magnitude > ZeroF)
            {
                HandleCharacterRotation(adjustedDirection);
                HandleCharacterController(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(ZeroF);
            }
        }

        void HandleCharacterRotation(Vector3 adjustedDirection)
        {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.LookAt(transform.position + adjustedDirection);
        }
        void HandleCharacterController(Vector3 adjustedDirection)
        {
            var adjustedMovement = adjustedDirection * (moveSpeed * Time.deltaTime);
            controller.Move(adjustedMovement);
        }
        void SmoothSpeed(float value)
        {              
            //ref allows us to pass the variable by reference, changing its value inside the function
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }

    }
}