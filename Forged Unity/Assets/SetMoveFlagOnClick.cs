// SetMoveFlagOnClick.cs
using UnityEngine;

public class SetMoveFlagOnClick : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        SceneState.shouldMoveObject = true;
        Debug.Log("Flag set: The object will move next time Scene B loads.");
    }
}
