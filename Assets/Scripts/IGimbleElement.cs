using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGimbleElement
{
    void Activate(GimbleInputController source, Transform objectToRotate);
}
