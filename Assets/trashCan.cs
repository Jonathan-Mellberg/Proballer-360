using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class trashCan : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PickUppable"))
        {
            Destroy(collision.gameObject);
        }
    }
}
