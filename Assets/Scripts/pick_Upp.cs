using UnityEngine;
using Unity.VisualScripting;

public class pick_Upp : MonoBehaviour
{
    [SerializeField] private float interactRadius = 3f;
    [SerializeField] private string interacteableTag = "Interacteable";
    [SerializeField] private string pickuppableTag = "PickUppable";
    [SerializeField] private GameObject holdPoint;

    private GameObject cam;
    private GameObject obj;
    private bool canInteract = false;
    private bool canPickUpp = false;
    private bool holdingObj = false;
    private RaycastHit hit;

    private void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera");
    }

    void FixedUpdate()
    {
        canInteract = false;
        canPickUpp = false;
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, interactRadius))
        {
            obj = hit.collider.gameObject;
            if (obj.CompareTag(interacteableTag))
            {
                canInteract = obj.CompareTag(interacteableTag);
            }
            else if (obj.CompareTag(pickuppableTag))
            {
                canPickUpp = obj.CompareTag(pickuppableTag);
            }

            if (Input.GetButtonDown("Interact") && obj.CompareTag(pickuppableTag) && canPickUpp)
            {
                obj.transform.position = holdPoint.transform.position;
                obj.transform.SetParent(holdPoint.transform);
                holdingObj = true;
            }
            if (Input.GetButtonDown("DropHeld") && holdingObj)
            {
                obj.transform.SetParent(null);
                holdingObj = false;
            }
            if (Input.GetButtonDown("Interact") && canInteract && !obj.IsUnityNull())
            {
                if (obj.TryGetComponent(out Interaction interaction))
                    interaction.Interact();
            }
        }
    }
}
