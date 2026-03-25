using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    [SerializeField] private string[] orders;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite angrySprite;
    [SerializeField] private int orderAmount = 2;
    [SerializeField] private int amountVariation = 1;
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
        GenerateOrder();
    }

    private void GenerateOrder()
    {
        /*
        // generate order
        List<string> orderList = new List<string>();
        string order = "a";
        int amount = Random.Range(amountVariation * -1, amountVariation) + orderAmount;

        for (int i = 0; i < orderAmount;)
        {
            order = orders[Random.Range(0, amount)];
            if (orderList.Contains(order))
                return;

            orderList.Add(order);
            i++;
        }

        orderReader.UpdateOrder(orderList.ToArray());
        Npc_Dia.order = order;
        */

        List<string> orderList = new List<string>();
        string verbalOrder = "";
        string order;

        for (int i = 0; i < orderAmount;)
        {
            order = orders[Random.Range(0, orders.Length)];

            if (orderList.Contains(order))
                return;

            orderList.Add(order);
            verbalOrder = verbalOrder + ", " + order;

            if (verbalOrder.StartsWith(", "))
                verbalOrder = verbalOrder.Remove(0, 2);

            i++;
        }
        //orderReader.UpdateOrder(orderList.ToArray());
        Npc_Dia.order = verbalOrder;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) CompleteOrder();
    }

    public void CompleteOrder()
    {
        if (spriteRenderer != null) spriteRenderer.sprite = normalSprite;
        Npc_Dia.CompletionSpeech();
        Leave();
    }

    private void Leave()
    {
        customerList.customerActive = false;
        if (leaveParticles != null) Instantiate(leaveParticles, transform);
        Destroy(custTimer);
        Destroy(gameObject);
    }

    public IEnumerator MoveCustomer(Transform targetPos, float moveTime)
    {
        float elapsedTime = 0f;
        Transform originalPos = transform;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(originalPos.position, targetPos.position, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos.position;
    }
}
