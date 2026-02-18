using UnityEngine;

public class Interaction : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Ran virtual method base.");
    }
}
