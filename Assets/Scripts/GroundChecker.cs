using UnityEngine;

namespace AE
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] float groundDistance = 0.6f;
        [SerializeField] LayerMask groundMask;   

        public bool IsGrounded { get; private set; }    

        private void Update()
        {
            IsGrounded = Physics.SphereCast(transform.position, groundDistance, Vector3.down, out RaycastHit hit, groundDistance, groundMask);
        }
    }
}