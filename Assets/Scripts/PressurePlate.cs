using AE;
using KBCore.Refs;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public UnityAction OnPressurePlate = delegate { };
    [Header("References")]
    [SerializeField, Anywhere] InteractableObjects interactableObjects;
    [SerializeField, Anywhere] RayCaster rayCaster;

    [Header("Settings")]
    [SerializeField] float radius = 3f;
    [SerializeField] LayerMask heavyObjectsLayer;

    InteractableObjects.ObjectData[] heavyObjects;

    bool isDoorOpen = false;
    bool allHeavyObjectsPresent = false;
    int heavyObjectsCount = 0;

    private void Awake()
    {
        heavyObjects = interactableObjects.GetHeavyObjects();
    }
    private void Update()
    {
        if (!isDoorOpen)
            Checker();
    }

    void Checker()
    {
        heavyObjectsCount = 0;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, heavyObjectsLayer);

        foreach (var heavyObject in heavyObjects)
        {
            bool objectFound = colliders.Any(collider => collider.transform.name == heavyObject.name);
            if (!objectFound)
            {
                heavyObjectsCount = 0;
                break;
            }
            else
            {
                heavyObjectsCount++;
            }
            Debug.Log(heavyObjectsCount);
        }
        if (heavyObjectsCount == 3)
        {
            isDoorOpen = true;
            

            OnPressurePlate.Invoke();
        }
    }
}
