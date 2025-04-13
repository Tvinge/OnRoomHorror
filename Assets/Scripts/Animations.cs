using UnityEngine;
using DG.Tweening;
using KBCore.Refs;

public class Animations : MonoBehaviour
{
    [Header("References")]
    [SerializeField, Anywhere] Transform obj;

    private void Awake()
    {
        DOTween.Init();
    }
    private void Start()
    {
        obj.DOMove(new Vector3(-6, -3, 18), 1f).SetEase(Ease.InBounce).OnComplete(() =>
        {
            // Log the final position
            Debug.Log($"Final position: {obj.position}");
        });
    }
}
