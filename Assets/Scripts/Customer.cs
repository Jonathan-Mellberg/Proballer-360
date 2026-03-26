using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    public GameObject[] orders;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite angrySprite;
    [SerializeField] private int orderAmount = 2;
    [SerializeField] private GameObject leaveParticles;

    private NPC_Dia Npc_Dia;
    private CustomerListV2 customerList;
    private Cust_Timer custTimer;
    private SpriteRenderer spriteRenderer;
    private OrderReader orderReader;
    private bool angry;

    public void GetVariables()
    {
        customerList = CustomerListV2.instance;
        Npc_Dia = transform.GetComponent<NPC_Dia>();
        orderReader = customerList.orderReader;
    }

    public void GenerateOrder()
    {
        List<GameObject> orderList = new List<GameObject>();
        string verbalOrder = "";
        GameObject order;

        for (int i = 0; i < orderAmount;)
        {
            order = orders[Random.Range(0, orders.Length)];

            if (orderList.Contains(order))
                return;

            orderList.Add(order);
            verbalOrder = verbalOrder + ", " + order.name;

            if (verbalOrder.StartsWith(", "))
                verbalOrder = verbalOrder.Remove(0, 2);

            i++;
        }
        orderReader.UpdateOrder(orderList.ToArray());
        Npc_Dia.order = verbalOrder;
    }

    public void CompleteOrder()
    {
        if (spriteRenderer != null) spriteRenderer.sprite = normalSprite;
        Npc_Dia.CompletionSpeech();
        orderReader.UpdateOrder(null);
        Leave();
    }

    private void Leave()
    {
        customerList.customerActive = false;
        if (leaveParticles != null) Instantiate(leaveParticles, transform);
        Destroy(custTimer);
        Destroy(gameObject);
    }
}
