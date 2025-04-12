using KBCore.Refs;
using UnityEngine;

namespace AE
{
    public class Interactions : MonoBehaviour
    {

        [Header("References")]
        [SerializeField, Anywhere] InteractableObjects interactableObjects;
        [SerializeField, Anywhere] RayCaster rayCaster;
        [SerializeField, Anywhere] PlayerController playerController;

        bool firstTimeCandleInteraction = true;

        private void Awake()
        {
            rayCaster.InteractionHappend += OnInteract;
            interactableObjects.SetCondition(interactableObjects.Objects[0].name, true); //example of setting a condition
        }

        void OnInteract(GameObject obj)
        {
            if (firstTimeCandleInteraction && obj.transform.GetComponent<Light>() != null)
            {
                playerController.hasLight = true;
                playerController.transform.GetChild(0).GetComponent<Light>().enabled = true;
            }          
         
        }
    }
}


