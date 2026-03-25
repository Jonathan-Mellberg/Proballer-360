using UnityEngine;

public class takeCup : MonoBehaviour
{
    public InteractWithItem interact; // form player

    public Transform coffeeplace; //empty gameobject to move cuo to

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.name == "cup_coffee" && !interact.holdingObj)
        {
            other.transform.SetParent(coffeeplace);
            other.transform.position =  coffeeplace.position;
            other.transform.rotation = Quaternion.identity;

            gameObject.GetComponent<coffeeMaking>().canFill = true;
            gameObject.GetComponent<coffeeMaking>().coffee = other.transform.Find("coffee").gameObject;
        }
    }
}
