using KBCore.Refs;
using System.Threading.Tasks;
using UnityEngine;
namespace AE
{
    public class Interactions : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, Anywhere] InteractableObjects interactableObjects;
        [SerializeField, Anywhere] RayCaster rayCaster;
        [SerializeField, Anywhere] PlayerController playerController;
        [SerializeField, Anywhere] PressurePlate pressurePlate;
        [SerializeField, Anywhere] AudioController audioController;
        [SerializeField, Anywhere] GameObject directionalLight;
        [SerializeField] GameObject door;
        [SerializeField] GameObject doorWall;


        bool firstTimeCandleInteraction = true;

        private void Awake()
        {
            rayCaster.InteractionHappend += OnInteract;
            pressurePlate.OnPressurePlate += OnPressurePlate;
        }

        void OnInteract(GameObject obj)
        {
            if (firstTimeCandleInteraction && obj.transform.GetComponent<Light>() != null)
            {
                firstTimeCandleInteraction = false;
                playerController.transform.GetChild(0).GetComponent<Light>().enabled = true;
                interactableObjects.SetLightCondition();
            }                   
        }

        // ...

        private async void OnPressurePlate()
        {
            audioController.PlayOpeningDoorSound();
            await Task.Delay(8000); // Wait for 8 seconds
            door.SetActive(false);
            doorWall.SetActive(false);
            directionalLight.SetActive(true);
        }
    }
}


