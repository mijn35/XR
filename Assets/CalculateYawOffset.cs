using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;

public class CalculateYawOffset : MonoBehaviour
{
    public Transform hipsTransform;
    public IKHipsTargetFollowPhoneRotation IKHips;

    private InputAction hmdRotationAction;
    private InputAction aButtonAction;

    private void Awake()
    {
        var actionMap = new InputActionMap("XRActions");
        hmdRotationAction = actionMap.AddAction("HMD_Rotation", binding: "<XRHMD>/deviceRotation");
        aButtonAction = actionMap.AddAction("A_Button", binding: "<XRController>{RightHand}/primaryButton");
        actionMap.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (aButtonAction.triggered)
        {
            Quaternion hmdRotation = hmdRotationAction.ReadValue<Quaternion>();
            Quaternion hipsRotation = hipsTransform.rotation;

            float hmdYaw = hmdRotation.eulerAngles.y;
            float hipsYaw = hipsRotation.eulerAngles.y;
            IKHips.hipsBodyYawOffset = hmdYaw - hipsYaw;

            if (IKHips.hipsBodyYawOffset > 180) IKHips.hipsBodyYawOffset -= 360;
            if (IKHips.hipsBodyYawOffset < -180) IKHips.hipsBodyYawOffset += 360;

            Debug.Log("HMD Yaw; " + hmdYaw + ", Hips Yaw " + hipsYaw + ", HippsBodyYawOffset " + IKHips.hipsBodyYawOffset);
        }
    }

    private void OnEnable()
    {
        hmdRotationAction.Enable();
        aButtonAction.Enable();
    }
    private void OnDisable()
    {
        hmdRotationAction.Disable();
        aButtonAction.Disable();
    }
}
