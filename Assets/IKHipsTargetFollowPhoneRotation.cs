using UnityEngine;

[System.Serializable]
public class PhoneMap
{
    public Transform phoneTarget;
    public Transform ikTarget;
    public Vector3 trackingRotationOffset;
    public void Map()
    {
        ikTarget.rotation = phoneTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class IKHipsTargetFollowPhoneRotation : MonoBehaviour
{
    [Range(0,1)]
    public float turnSmoothness = 0.1f;
    public PhoneMap hips;

    public float hipsBodyYawOffset;

    // Update is called once per frame
    void LateUpdate()
    {
        float yaw = hips.phoneTarget.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z),turnSmoothness);

        hips.Map();
    }
}
