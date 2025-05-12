using UnityEngine;
using DialogueEditor;

public class DoctorCharacter : MonoBehaviour
{
    public NPCConversation myCoversation;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ConversationManager.Instance.StartConversation(myCoversation);
        }
    }
}