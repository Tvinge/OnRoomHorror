using KBCore.Refs;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

namespace AE
{
    public class CameraManager : MonoBehaviour
    {

        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField, Anywhere] CinemachineCamera freeLookVCam;        

        [Header("Settings")]
        [SerializeField, Range(0.5f, 3)] float speedMultiplayer = 1f;

        bool isRMBPressed;
        bool cameraMovementLock;

        //private void OnEnable()
        //{
        //    input.Look += OnLook;
        //    input.EnableMouseControlCamera += OnEnableMouseControlCamera;
        //    input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        //}
        //void OnDisable() 
        //{
        //    input.Look -= OnLook;
        //    input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
        //    input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        //}

        void OnEnableMouseControlCamera()
        {
            isRMBPressed = true;
            //lock the cursor to the center of the screen and hide it 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(DisableMouseForFrame());
        }
        IEnumerator DisableMouseForFrame()
        {
            cameraMovementLock = true;
            yield return new WaitForEndOfFrame();
            cameraMovementLock = false;
        }
        void OnDisableMouseControlCamera()
        {
            isRMBPressed = false;

            //unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //reset the camera axis to prevent jumping when reanabling mouse control
            //freeLookVCam.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = 0;
            //freeLookVCam.GetComponent<CinemachineOrbitalFollow>().VerticalAxis.Value = 0;
        }

        void OnLook(Vector2 cameraMovement, bool isDeviceMouse)
        {
            if ( cameraMovementLock )
                return;
            if (isDeviceMouse && !isRMBPressed)
                return;
            float deviceMultiplier = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;

            //set the camera axis values
            //freeLookVCam.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = cameraMovement.x * speedMultiplayer * deviceMultiplier;
            //freeLookVCam.GetComponent<CinemachineOrbitalFollow>().VerticalAxis.Value = cameraMovement.y * speedMultiplayer * deviceMultiplier;
        }

    }
}


