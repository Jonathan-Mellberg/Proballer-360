using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderReader : Interaction
{
    [SerializeField] private BoxCollider platterHitbox;
    [SerializeField] private CustomerListV2 customerList;
    private string[] orders;
    [SerializeField] private int penalty = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateOrder(string[] orderList)
    {
        orders = orderList;
    }

    public override void Interact()
    {
        
    }

    private void TurnInOrder()
    {
        if (ReadPlatter() == true)
            customerList.customerObj.GetComponent<Customer>().CompleteOrder();

    }

    private bool ReadPlatter()
    {
        // Find all objects in platter hitbox
        List<string> items = new List<string>();
        Transform p = platterHitbox.transform;
        Collider[] colliders = Physics.OverlapBox(p.position, p.localScale / 2, Quaternion.identity);

        // Convert collider list to string list
        foreach (Collider collider in colliders) 
        { 
            items.Add(collider.gameObject.name); 
        }

        // Check for incorrect items
        bool incorrectItem = false;
        foreach (string item in items)
        {
            if (!orders.Contains(item))
            {
                incorrectItem = true;
            }
        }

        if (incorrectItem)
        {
            customerList.customerObj.GetComponent<NPC_Dia>().IncorrectSpeech();
        }
        return incorrectItem;
    }
}
