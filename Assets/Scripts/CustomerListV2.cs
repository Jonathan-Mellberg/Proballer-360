using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomerListV2 : MonoBehaviour
{
    public static CustomerListV2 instance { get; private set; }

    public GameObject[] customers;
    [HideInInspector] public int customerIndex = 0;

    [Header("References")]
    public GameObject player;
    public Image textBox;
    public TextMeshProUGUI nameTextBox;
    public TextMeshProUGUI dialogueTextBox;
    public TextMeshProUGUI endTextPopup;
    public Image patienceBar;

    private GameObject customerObj;
    private NPC_Dia Npc_Dia;

    [SerializeField] private Transform SpawnPos;
    [SerializeField] private Transform[] CounterPos;
    [SerializeField] private Transform WaitPos;

    [Header("Variables")]
    [SerializeField] private float waitTime = 10f;
    [SerializeField] private float customerSpeed = 1f;

    [HideInInspector] public bool customerActive;
    private bool spawning;

    // Singleton Class
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

    }


    private void FixedUpdate()
    {
        if (!customerActive && customerIndex < customers.Length)
        {
            StartCoroutine(CustomerSpawn());
        }
    }

    private void InitializeCustomer()
    {
        customerObj = Instantiate(customers[customerIndex], SpawnPos.position, Quaternion.identity);
        customerObj.transform.SetParent(gameObject.transform);

        Customer customerScript = customerObj.GetComponent<Customer>();
        customerScript.GetVariables();

        customerIndex++;
        Npc_Dia = customerObj.GetComponent<NPC_Dia>();
    }

    private IEnumerator CustomerSpawn()
    {
        customerActive = true;
        yield return new WaitForSeconds(waitTime);
        InitializeCustomer();

        Npc_Dia.canSpeak = false;
        // move customer to counter pos
        foreach (Transform pos in CounterPos)
        {
            yield return StartCoroutine(MoveCustomer(customerObj.transform, pos));
        }

        Npc_Dia.canSpeak = true;
    }

    public IEnumerator MoveToWaitPos()
    {
        Npc_Dia.canSpeak = false;
        yield return StartCoroutine(MoveCustomer(customerObj.transform, WaitPos));
        Npc_Dia.canSpeak = true;
    }


    private IEnumerator MoveCustomer(Transform customer, Transform endPoint)
    {
        while (customerObj.transform.position != endPoint.position)
        {
            customer.localPosition = Vector3.MoveTowards(customer.transform.localPosition, endPoint.position, customerSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
