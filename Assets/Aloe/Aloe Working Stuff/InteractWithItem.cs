using UnityEngine;
using Unity.VisualScripting;
using Unity.Mathematics;

public class InteractWithItem : MonoBehaviour
{
    [SerializeField] private float interactRadius = 3f;
    [SerializeField] private string interacteableTag = "Interacteable";
    [SerializeField] private string pickuppableTag = "PickUppable";
    [SerializeField] private string stationTag = "PickUppableStation";
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
        Cursor.visible = true;
    }

    void FixedUpdate()
    {
        canInteract = false;
        canPickUpp = false;

        if (!holdingObj && holdPoint.transform.childCount > 0)
        {
            holdPoint.transform.DetachChildren();
        }
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, interactRadius))
        {
            obj = hit.collider.gameObject;
            if (obj.CompareTag(interacteableTag))
            {
                canInteract = obj.CompareTag(interacteableTag);
            }
            else if (obj.CompareTag(pickuppableTag) || obj.CompareTag(stationTag))
            {
                canPickUpp = obj.CompareTag(pickuppableTag) || obj.CompareTag(stationTag);
            }

            if (Input.GetButtonDown("Interact") && obj.CompareTag(pickuppableTag) && canPickUpp && !holdingObj || Input.GetButtonDown("Interact") && obj.CompareTag(stationTag) && canPickUpp && !holdingObj)
            {
                if (obj.CompareTag(stationTag))
                {
                    GameObject thinginbox = obj.GetComponent<whatInBox>().thingInBox;
                    obj = GameObject.Instantiate(thinginbox);
                }
                obj.transform.position = holdPoint.transform.position;
                obj.transform.rotation = quaternion.identity;
                obj.transform.SetParent(holdPoint.transform);
                if (obj.TryGetComponent<Rigidbody>(out rig))
                {
                    rig.isKinematic = true;
                }
                holdingObj = true;
                heldObj = obj.name;
                Debug.Log(heldObj);
            }

            if (Input.GetButtonDown("DropHeld") && holdingObj)
            {
                obj.transform.SetParent(null);
                if (rig.isKinematic)
                {
                    rig.isKinematic = false;
                }
                holdingObj = false;
                heldObj = null;
            }

            if (Input.GetButtonDown("Interact") && canInteract && !obj.IsUnityNull())
            {
                if (holdingObj)
                {
                    switch (heldObj)
                    {
                        case ("cup_coffee"):
                            if(hit.collider.gameObject.TryGetComponent<toppingJar>(out toppingJar topping))
                            {
                                Transform toppingPlace = GameObject.Find(heldObj).transform.Find("coffee").transform;
                                GameObject topper = GameObject.Instantiate(topping.topping,toppingPlace,false);
                                topper.transform.localPosition += new Vector3(0f, 0.2f, 0f);
                                Debug.Log(topper.transform.localPosition);
                                Debug.Log("topping added");
                            }
                            break;
                        // add case for pie and cake
                    }
                }
                if (obj.TryGetComponent(out interaction))
                    interaction.Interact();
            }
        }
    } 
}
