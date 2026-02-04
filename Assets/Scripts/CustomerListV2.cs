using System.Collections;
using UnityEngine;

public class CustomerListV2 : MonoBehaviour
{
    public static CustomerListV2 instance { get; private set; }

    public GameObject[] customers;

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
    }

    private void FixedUpdate()
    {
        Debug.Log(customerIndex);
        if (customerCount < customerLimit && customerIndex < customers.Length && !spawning) StartCoroutine(CustomerSpawn());
    }

    private void SpawnCustomer()
    {
        GameObject customerObj = Instantiate(customers[customerIndex], customerSpawn.position, Quaternion.identity);
        Customer customerScript = customerObj.GetComponent<Customer>();

        customerScript.StartCoroutine(customerScript.MoveCustomer(queuePositions[customerCount], customerWalkSpeed));
        customerIndex++;
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
