using UnityEngine;
using Unity.VisualScripting;

public class InteractWithItem : MonoBehaviour
{
    [SerializeField] private float interactRadius = 3f;
    [SerializeField] private string interacteableTag = "Interacteable";
    [SerializeField] private string pickuppableTag = "PickUppable";
    [SerializeField] private GameObject holdPoint;
    [SerializeField] private Rigidbody rig;


    Interaction interaction;
    private string heldObj;
    private GameObject cam;
    private GameObject obj;
    private GameObject billObject;
    private bool canInteract = false;
    private bool canPickUpp = false;
    private RaycastHit hit;

    public bool holdingObj = false;

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
                if (obj.TryGetComponent<Rigidbody>(out rig))
                {
                    rig.isKinematic = true;
                }
                holdingObj = true;
                heldObj = obj.name;
            }
            if (Input.GetButtonDown("DropHeld") && holdingObj)
            {
                obj.transform.SetParent(null);
                if (rig.isKinematic)
                {
                    rig.isKinematic = false;
                }
                holdingObj = false;
            }
            if (Input.GetButtonDown("Interact") && canInteract && !obj.IsUnityNull())
            {
                if (holdingObj)
                {

                    interaction.Interact();
                }
                if (obj.TryGetComponent(out interaction))
                    interaction.Interact();
            }
        }
    } 
}
