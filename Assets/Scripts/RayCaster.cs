﻿using KBCore.Refs;
using System.Linq;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace AE
{
    public class RayCaster : MonoBehaviour
    {

        public event UnityAction<GameObject> InteractionHappend = delegate { };

        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField, Anywhere] Camera mainCamera;
        [SerializeField, Anywhere] CinemachineCamera freeLookVCam;
        [SerializeField, Anywhere] TextMeshProUGUI interactionPopUp;
        [SerializeField, Anywhere] InteractableObjects interactableObjects;
        [SerializeField, Anywhere] Interactions interactions;

        public InteractableObjects.ObjectData[] objects;

        [Header("Settings")]
        [SerializeField] float maxDistance = 100f;
        [SerializeField] float interactionDistance = 30f;
        [SerializeField] string interactable = "Interactable";
        [SerializeField] string heavyObj = "heavyObj";
        [SerializeField] bool displayingPopUp = false;
        [SerializeField] string defaultSentence = "Click LMB or drag to interact with the object";

        public bool IsLookingAtInteractable { get; private set; } = false;

        Vector3 origin;
        Vector3 direction;
        GameObject hitObject;


        private void Awake()
        {
            input.Interact += OnInteract;
            objects = interactableObjects.InitializeObjects();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void Update()
        {
            HandleHiting();
            Disp();
        }

        void HandleHiting()
        {
            origin = mainCamera.transform.position;
            direction = freeLookVCam.transform.TransformDirection(Vector3.forward) * maxDistance;
            Debug.DrawRay(origin, direction * maxDistance, Color.blue, 0.1f);

            IsLookingAtInteractable = Physics.Raycast(
                origin: origin,
                direction: direction,
                hitInfo: out RaycastHit hitInfo,
                maxDistance: 100);

            if (hitInfo.collider.CompareTag(interactable) == true)
            {
                hitObject = hitInfo.collider.gameObject;
                displayingPopUp = true;
                Debug.Log($"{hitObject.name}");
            }
            else
            {
                hitObject = null;
                interactionPopUp.text = defaultSentence;
                displayingPopUp = false;
            }
        }

        void Disp()
        {
            if (displayingPopUp)
            {
                interactionPopUp.gameObject.SetActive(true);
            }
            else
            {
                interactionPopUp.gameObject.SetActive(false);
            }
        }
        void OnInteract()
        {
            if (IsLookingAtInteractable && hitObject.name != null)
            {
                var interactableObject = objects.FirstOrDefault(obj => obj.name == $"{hitObject.name}");
                interactionPopUp.text = interactableObject?.description;
                displayingPopUp = true;
                InteractionHappend?.Invoke(hitObject);
            }
            else
            {
                displayingPopUp = false;
            }
        }

    }
}


