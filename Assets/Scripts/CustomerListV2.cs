using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomerListV2 : MonoBehaviour
{
    public static CustomerListV2 instance { get; private set; }

    public GameObject[] customers;

    [HideInInspector] public int customerCount = 0;
    [HideInInspector] public int customerIndex;
    [HideInInspector] public int currentCustomerId;

    [Header("References")]
    public GameObject player;
    public TextMeshProUGUI nameTextBox;
    public TextMeshProUGUI dialogueTextBox;
    public Image patienceBar;
    [SerializeField] private Transform customerSpawn;
    [SerializeField] private Transform[] queuePositions;

    [Header ("Variables")]
    [SerializeField] private int customerSpawnTime = 30;
    [SerializeField] private int spawnTimeVariation = 5;
    [SerializeField] private int customerLimit = 5;
    [SerializeField] private float customerWalkSpeed = 1f;

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
        if (customerCount < customerLimit && customerIndex < customers.Length && !spawning) StartCoroutine(CustomerSpawn());
    }

    private void SpawnCustomer()
    {
        GameObject customerObj = Instantiate(customers[customerIndex], customerSpawn.position, Quaternion.identity);
        customerObj.transform.SetParent(gameObject.transform);

        Customer customerScript = customerObj.GetComponent<Customer>();
        customerScript.GetVariables();

        customerScript.StartCoroutine(customerScript.MoveCustomer(queuePositions[customerCount], customerWalkSpeed));
        customerIndex++;
    }

    private IEnumerator CustomerSpawn()
    {
        spawning = true;
        float waitTime = Random.Range(spawnTimeVariation * -1, spawnTimeVariation) + customerSpawnTime;
        yield return new WaitForSeconds(waitTime);
        SpawnCustomer();
        customerCount++;
        spawning = false;
    }
}
