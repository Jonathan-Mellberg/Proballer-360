using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderReader : Interaction
{
    [SerializeField] private BoxCollider platterHitbox;
    [SerializeField] private ScoreV2 score;
    private string[] orders;

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
        TurnInOrder();
    }

    private void TurnInOrder()
    {

    }

    private void ReadPlatter()
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
        int incorrectItems = 0;
        foreach (string item in items)
        {
            if (!orders.Contains(item))
            {
                incorrectItems++;
            }
        }
    }
}
