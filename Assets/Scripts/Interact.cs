using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private float interactRadius = 3f;
    [SerializeField] private string interacteableTag = "Interacteable";
    
    private GameObject cam;
    private GameObject obj;
    private bool canInteract;
    private RaycastHit hit;

    private void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera");
    }

    void FixedUpdate()
    {

        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, interactRadius))
        {
            obj = hit.collider.gameObject;
            canInteract = obj.CompareTag(interacteableTag);
        }

        if (Input.GetButtonDown("Interact") && canInteract && !obj.IsUnityNull())
        {
            if (obj.TryGetComponent(out Interaction interaction))
                interaction.Interact();
        }
    }
}