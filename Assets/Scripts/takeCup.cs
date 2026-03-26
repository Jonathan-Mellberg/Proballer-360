using UnityEngine;

public class takeCup : MonoBehaviour
{
    public InteractWithItem interact; // form player
    public Transform coffeeplace; //empty gameobject to move cuo to

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.Find("coffee") && !interact.holdingObj)
        {
            other.transform.SetParent(coffeeplace);
            other.transform.position =  coffeeplace.position;
            other.transform.rotation = Quaternion.identity;

            gameObject.GetComponent<coffeeMaking>().canFill = true;
            gameObject.GetComponent<coffeeMaking>().coffee = other.transform.Find("coffee").gameObject;
        }
    }
}
