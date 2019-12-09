using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 lastFramePos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastFramePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 movementAmountDelta = Input.mousePosition - lastFramePos;
            Vector3 rotation = transform.eulerAngles;
            rotation.y += (movementAmountDelta.x / 5);
            transform.eulerAngles = rotation;
        }
        lastFramePos = Input.mousePosition;
    }
}
