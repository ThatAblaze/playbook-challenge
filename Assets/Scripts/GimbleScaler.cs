using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class GimbleScaler : MonoBehaviour, IGimbleElement
{
    public Vector3 axis = Vector3.right;

    private RaycastHit hit;
    private CoroutineHandle rotater;

    public void Activate(GimbleInputController source, Transform objectToScale)
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        rotater = Timing.RunCoroutine(_ScaleWithTouches(source, objectToScale));
#else
        rotater = Timing.RunCoroutine(_ScaleWithMouse(source, objectToScale));
#endif
    }

    private IEnumerator<float> _ScaleWithMouse(GimbleInputController source, Transform objectToScale)
    {
        Vector2 startPos = Input.mousePosition;
        Vector3 initialScale = objectToScale.transform.localScale;

        while (!Input.GetMouseButtonUp(0))
        {
            objectToScale.localScale = new Vector3(
                initialScale.x + (axis.x * (Input.mousePosition.x - startPos.x)),
                initialScale.y + (axis.y * (Input.mousePosition.y - startPos.y)),
                initialScale.z + (axis.z * (Input.mousePosition.x - startPos.x)));

            yield return Timing.WaitForOneFrame;
        }

        source.InputFinished();
    }


    private IEnumerator<float> _ScaleWithTouches(GimbleInputController source, Transform objectToScale)
    {
        Vector2 startPos = Input.touches[0].position;
        Vector3 initialRotation = objectToScale.transform.localScale;

        while (Input.touchCount > 0)
        {
            objectToScale.localScale = new Vector3(
                initialRotation.x + (axis.x * (Input.touches[0].position.x - startPos.x)),
                initialRotation.y + (axis.y * (Input.touches[0].position.y - startPos.y)),
                initialRotation.z + (axis.z * (Input.touches[0].position.x - startPos.x)));

            yield return Timing.WaitForOneFrame;
        }

        source.InputFinished();
    }
}
