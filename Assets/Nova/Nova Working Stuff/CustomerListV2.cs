using System.Collections;
using UnityEngine;

public class CustomerListV2 : MonoBehaviour
{
    public static CustomerListV2 instance { get; private set; }

    public GameObject[] charachters;
    public Customer[] customers;

    [HideInInspector] public int customerCount = 0;
    [HideInInspector] public int customerIndex;

    [SerializeField] private Transform customerSpawn;
    [SerializeField] private Transform[] queuePositions;
    [SerializeField] private int customerSpawnTime = 30;
    [SerializeField] private int spawnTimeVariation = 5;
    [SerializeField] private int customerLimit = 5;
    [SerializeField] private float customerWalkSpeed = 1f;

    private bool spawning;

    // Singleton Class
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
            Destroy(gameObject);

        queuePositions = new Transform[customerLimit];
    }

    private void FixedUpdate()
    {
        if (customerCount > customerLimit && !spawning) StartCoroutine(CustomerSpawn());
    }

    private void SpawnCustomer()
    {
        Customer customer = new Customer();
        Instantiate(customer, customerSpawn.position, Quaternion.identity);
        customer.StartCoroutine(customer.MoveCustomer(queuePositions[customerCount + 1], customerWalkSpeed));
    }

    private IEnumerator CustomerSpawn()
    {
        spawning = true;
        float waitTime = Random.Range(spawnTimeVariation, -1 * spawnTimeVariation) + customerSpawnTime;
        yield return new WaitForSeconds(waitTime);
        SpawnCustomer();
        customerCount++;
        spawning = false;
    }
}
