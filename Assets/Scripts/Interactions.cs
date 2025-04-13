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
        [SerializeField, Anywhere] GameObject endGameText;

        bool firstTimeCandleInteraction = true;

        private void Awake()
        {
            rayCaster.InteractionHappend += OnInteract;
            pressurePlate.OnPressurePlate += OnPressurePlate;

            door.SetActive(true);
            doorWall.SetActive(true);
            directionalLight.SetActive(false);
            endGameText.SetActive(false);

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
        async void OnPressurePlate()
        {
            audioController.PlayOpeningDoorSound();
            await Task.Delay(7000);
            door.SetActive(false);
            doorWall.SetActive(false);
            directionalLight.SetActive(true);
            endGameText.SetActive(true);
        }
    }
}


