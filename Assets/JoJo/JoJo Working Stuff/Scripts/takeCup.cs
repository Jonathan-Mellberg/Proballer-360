using UnityEngine;

public class takeCup : MonoBehaviour
{
    public InteractWithItem interact;

    public Transform coffeeplace;

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

        if (other.name == "cup" && !interact.holdingObj)
        {
            other.transform.SetParent(coffeeplace);
            other.transform.position =  coffeeplace.position;
            other.transform.rotation = Quaternion.identity;

            gameObject.GetComponent<coffeeMaking>().canFill = true;
        }
    }
}
