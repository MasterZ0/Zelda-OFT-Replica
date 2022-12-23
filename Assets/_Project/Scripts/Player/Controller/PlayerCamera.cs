﻿using System;
using TriplanoTest.Data;
using UnityEngine;

namespace TriplanoTest.Player
{
    [Serializable]
    public class PlayerCamera : PlayerControllerComponent
    {
        [Header("Camera")]
        [SerializeField] private Transform cameraTarget;

        public bool XLocked { get; private set; }
        public bool YLocked { get; private set; }

        private float xRotation;
        private float yRotation;

        private const float Threshold = 0.01f;
        public Transform CameraTarget => cameraTarget;
        private PlayerData Settings => Controller.Data;

        #region Initialization
        public override void Init(PlayerController playerController)
        {
            base.Init(playerController);

            yRotation = cameraTarget.localEulerAngles.y;
        }
        #endregion

        public void LockY(bool locked) => YLocked = locked;

        public void Update()
        {
            UpdateCameraRotation();
        }

        private void UpdateCameraRotation()
        {
            Vector2 look = Controller.Inputs.Look;

            if (look.sqrMagnitude >= Threshold)
            {
                if (!XLocked)
                {
                    xRotation -= look.y * Settings.Sensitivity;
                    xRotation = Mathf.Clamp(xRotation, Settings.CameraRangeRotation.x, Settings.CameraRangeRotation.y);
                }

                if (!YLocked)
                {
                    yRotation += look.x * Settings.Sensitivity;
                    yRotation = ClampAngle(yRotation);
                }
                else
                {
                    yRotation = cameraTarget.eulerAngles.y;
                }
            }

            cameraTarget.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }

        private static float ClampAngle(float angle)
        {
            if (angle < -360f)
                angle += 360f;

            if (angle > 360f)
                angle -= 360f;

            return angle;
        }
    }
}