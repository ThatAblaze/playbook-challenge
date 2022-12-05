This module demonstrates rotation, translation, and scaling using gimble controls in a 3d scene.

The manipulation controls prefab can be dropped on any object and it should allow the user to manupulate the object while the app is running.

The method employed is that an input controller detects mouse input or touches and does a raycast into the scene. If the raycast hits one of the interface elements then the element will perform its funcion until the mouse button or touch is released.

Known limitations:
- If more than one gimble is added to the scene at the same time then all control elements will effect a random element. This can be fixed by having a universal input controller rather than one per object.
- All translations are performed in absolute space and not relitive to the orentation of the object. This can be fixed by multiplying the vector by the object's rotation.
- All translations assume the camera's up is the same as the world up. This can be fixed by multiplying the translation vector by the camera's up vector.