using System.Collections.Generic;
using UnityEngine;

public class OrderReader : Interaction
{
    [SerializeField] private Transform platterHitbox;
    [SerializeField] private CustomerListV2 customerList;
    [SerializeField] private AudioClip bellSound;
    private GameObject[] orders;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        if (audioSource != null)
            audioSource.PlayOneShot(bellSound);

        if (orders == null)
            return;

        if (ReadPlatter() == true)
            customerList.customerObj.GetComponent<Customer>().CompleteOrder();
    }

    public void UpdateOrder(GameObject[] newOrder)
    {
        orders = newOrder;
    }

    private bool ReadPlatter()
    {
        // Find all objects in platter hitbox
        List<GameObject> items = new List<GameObject>();
        Collider[] colliders = Physics.OverlapBox(platterHitbox.position, platterHitbox.localScale / 2, Quaternion.identity);
        GameObject coffeeOrder = null;

        // Convert collider list to gameObject list
        foreach (Collider collider in colliders) 
        {
            if (collider.gameObject.CompareTag("PickUppable") && !collider.gameObject.name.StartsWith("Platter"))
                items.Add(collider.gameObject);
        }

        // Identify coffee object
        foreach (GameObject order in orders)
        {
            if (order.transform.Find("coffee"))
                coffeeOrder = order.transform.Find("coffee").gameObject;
        }


        // Check for incorrect items
        bool incorrectItem = false;
        foreach (GameObject item in items)
        {
            if (!orders.ToString().Contains(item.name))
            {
                incorrectItem = true;
            }

            if (item == coffeeOrder)
            {
                GameObject coffee = item.transform.Find("coffee").gameObject;

                Debug.Log(coffee.transform.localScale.y);

                if (coffee.GetComponent<MeshRenderer>().material != coffeeOrder.GetComponent<MeshRenderer>().material
                    || coffee.transform.localScale.y < 0.75
                    || coffee.transform.GetChild(0).name == coffeeOrder.transform.GetChild(0).name)
                {
                    incorrectItem = true;
                }
            }
        }

        if (incorrectItem)
        {
            customerList.customerObj.GetComponent<NPC_Dia>().IncorrectSpeech();
        }

        foreach(GameObject item in items)
        {
            if (item.CompareTag("PickUppable"))
                Destroy(item);
        }

        return incorrectItem;
    }
}