using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2,
    }

    [SerializeField] private RotationAxes axes = RotationAxes.MouseXAndY;
    [SerializeField] private float sensitivityHor = 9.0f;
    [SerializeField] private float sensitivityVert = 9.0f;
    [SerializeField] private float minimumVert = -45.0f;
    [SerializeField] private float maximumVert = 45.0f;

    private float verticalRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        var body=GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);
            transform.localEulerAngles = new Vector3(verticalRot, transform.localEulerAngles.y, 0);
        }
        else
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);
            var delta = Input.GetAxis("Mouse X") * sensitivityHor;
            var horizontalRot = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }
    }
}
