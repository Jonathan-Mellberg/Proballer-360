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

    public void GetVariables()
    {
        customerList = CustomerListV2.instance;
        Npc_Dia = gameObject.GetComponent<NPC_Dia>();
        orderReader = customerList.orderReader;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        custTimer = gameObject.GetComponent<Cust_Timer>();
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
        custTimer.StopTimer();
        orderReader.UpdateOrder(null);
        Leave();
    }

    public void BecomeAngry()
    {
        spriteRenderer.sprite = angrySprite;
    }

    private void Leave()
    {
        customerList.customerActive = false;
        if (leaveParticles != null) Instantiate(leaveParticles, transform);
        Destroy(gameObject);
    }
}
