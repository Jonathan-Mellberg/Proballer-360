using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using NUnit.Framework;

public class Customer : MonoBehaviour
{
    // button to complete order
    // randomize score
    // on complete order, kill current customer and move onward to next customer
    // next customer lerps forward to point
    // at end of day, get final score, debug log list of customer

    // pool of orders
    [SerializeField] private string[] orders;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite angrySprite;
    [SerializeField] private int orderAmount = 2;
    [SerializeField] private int amountVariation = 1;

    private NPC_Dia Npc_Dia;    
    private ScoreV2 score;
    private CustomerListV2 customerList;
    private Cust_Timer custTimer;
    private SpriteRenderer spriteRenderer;
    private OrderReader orderReader;
    private bool angry;

    public void GetVariables()
    {
        customerList = CustomerListV2.instance;
        score = customerList.gameObject.GetComponent<ScoreV2>();
        orderReader = score.orderReader;
        TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer);
        TryGetComponent<NPC_Dia>(out NPC_Dia Npc_Dia);
    }

    private void GenerateOrder()
    {
        // generate order
        List<string> orderList = new List<string>();
        string order;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) CompleteOrder();
    }

    public void CompleteOrder()
    {
        int customerScore = score.ratings[customerList.customerIndex].score;

        bool win = customerScore >= score.killThreshold;

        if (spriteRenderer != null) spriteRenderer.sprite = win ? normalSprite : angrySprite;
        Npc_Dia.CompletionSpeech(win);
        if (win) Leave(); else Kill();
    }

    private void Leave()
    {
        customerList.customerActive = false;
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

    private void Kill()
    {
        Debug.Log("You die rawwwwr");
    }

    // give order, take order

    // says order in speech scripts

    // function to generate an order

    // send order to platter script
}
