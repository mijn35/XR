using UnityEngine;
using UnityEngine.InputSystem;

public class CalculateHeight : MonoBehaviour
{
    public Transform hmdTransform;
    public Transform model;
    private InputAction bButtonAction;

    private float bodyScale;
    private Vector3 scale;

    public float defaultHeight = 2.0f;

    private void Awake()
    {
        var actionMap = new InputActionMap("XRActions");
        bButtonAction = actionMap.AddAction("B_Button", binding: "<XRController>{RightHand}/secondaryButton");
        actionMap.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (bButtonAction.triggered)
        {
            float hmdHeight = hmdTransform.transform.localPosition.y;
            bodyScale = hmdHeight/defaultHeight;

            Debug.Log("hmdHeight " + hmdHeight + "bodyScale " + bodyScale);

            scale.x = bodyScale;
            scale.y = bodyScale;
            scale.z = bodyScale;

            model.transform.localScale = scale;
        }
    }

    private void OnEnable()
    {
        bButtonAction.Enable();
    }
    private void OnDisable()
    {
        bButtonAction.Disable();
    }
}
