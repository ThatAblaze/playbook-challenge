using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class GimbleRotater : MonoBehaviour, IGimbleElement
{
    public Vector3 axis = Vector3.right;

    private RaycastHit hit;
    private CoroutineHandle rotater;

    public void Activate(GimbleInputController source, Transform objectToRotate)
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        rotater = Timing.RunCoroutine(_RotateWithTouches(source, objectToRotate));
#else
        rotater = Timing.RunCoroutine(_RotateWithMouse(source, objectToRotate));
#endif
    }

    private IEnumerator<float> _RotateWithMouse(GimbleInputController source, Transform objectToRotate)
    {
        Vector2 startPos = Input.mousePosition;
        Vector3 initialRotation = objectToRotate.localRotation.eulerAngles;

        while(!Input.GetMouseButtonUp(0))
        {
            objectToRotate.localRotation = Quaternion.Euler(
                initialRotation.x + (axis.y * (Input.mousePosition.x - startPos.x)), 
                initialRotation.y + (axis.z * (Input.mousePosition.x - startPos.x)), 
                initialRotation.z + (axis.x * (Input.mousePosition.y - startPos.y)));

            yield return Timing.WaitForOneFrame;
        }

        source.InputFinished();
    }


    private IEnumerator<float> _RotateWithTouches(GimbleInputController source, Transform objectToRotate)
    {
        Vector2 startPos = Input.touches[0].position;
        Vector3 initialRotation = objectToRotate.localRotation.eulerAngles;

        while (Input.touchCount > 0)
        {
            objectToRotate.localRotation = Quaternion.Euler(
                initialRotation.x + (axis.x * (Input.touches[0].position.y - startPos.y)),
                initialRotation.y + (axis.y * (Input.touches[0].position.x - startPos.x)),
                initialRotation.z + (axis.z * (Input.touches[0].position.y - startPos.y)));

            yield return Timing.WaitForOneFrame;
        }

        source.InputFinished();
    }
}
