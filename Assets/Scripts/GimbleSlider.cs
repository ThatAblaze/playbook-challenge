using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class GimbleSlider : MonoBehaviour, IGimbleElement
{
    public Vector3 axis = Vector3.right;

    private RaycastHit hit;
    private CoroutineHandle rotater;

    public void Activate(GimbleInputController source, Transform objectToSlide)
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        rotater = Timing.RunCoroutine(_ScaleWithTouches(source, objectToSlide));
#else
        rotater = Timing.RunCoroutine(_ScaleWithMouse(source, objectToSlide));
#endif
    }

    private IEnumerator<float> _ScaleWithMouse(GimbleInputController source, Transform objectToSlide)
    {
        Vector2 startPos = Input.mousePosition;
        Vector3 initialPos = objectToSlide.transform.localPosition;

        while (!Input.GetMouseButtonUp(0))
        {
            objectToSlide.localPosition = new Vector3(
                initialPos.x + (axis.x * (Input.mousePosition.x - startPos.x)),
                initialPos.y + (axis.y * (Input.mousePosition.y - startPos.y)),
                initialPos.z + (axis.z * (Input.mousePosition.x - startPos.x)));

            yield return Timing.WaitForOneFrame;
        }

        source.InputFinished();
    }


    private IEnumerator<float> _ScaleWithTouches(GimbleInputController source, Transform objectToSlide)
    {
        Vector2 startPos = Input.touches[0].position;
        Vector3 initialPos = objectToSlide.transform.localScale;

        while (Input.touchCount > 0)
        {
            objectToSlide.localScale = new Vector3(
                initialPos.x + (axis.x * (Input.touches[0].position.x - startPos.x)),
                initialPos.y + (axis.y * (Input.touches[0].position.y - startPos.y)),
                initialPos.z + (axis.z * (Input.touches[0].position.x - startPos.x)));

            yield return Timing.WaitForOneFrame;
        }

        source.InputFinished();
    }
}
