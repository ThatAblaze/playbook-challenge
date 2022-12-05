using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimbleInputController : MonoBehaviour
{
    public Transform objectToRotate;

    private bool uiActive = false;

    void Update()
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        if (!uiActive && Input.touchCount > 0)
        {
        
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.touches[0].position), out hitInfo))
            {
                IGimbleElement interactableGimble = hitInfo.transform.GetComponent<IGimbleElement>();

                if (interactableGimble != null)
                {
                    uiActive = true;
                    if (objectToRotate == null)
                    {
                        interactableGimble.Activate(this, transform.parent);
                    }
                    else
                    {
                        interactableGimble.Activate(this, objectToRotate);
                    }
                }
            }
        }
#else
        if (!uiActive && Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                IGimbleElement interactableGimble = hitInfo.transform.GetComponent<IGimbleElement>();

                if (interactableGimble != null)
                {
                    uiActive = true;
                    if (objectToRotate == null)
                    {
                        interactableGimble.Activate(this, transform.parent);
                    }
                    else
                    {
                        interactableGimble.Activate(this, objectToRotate);
                    }
                }
            }
        }
#endif
    }

    public void InputFinished()
    {
        uiActive = false;
    }
}
