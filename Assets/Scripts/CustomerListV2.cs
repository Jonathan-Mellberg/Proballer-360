using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

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

    [SerializeField] private Transform SpawnPos;
    [SerializeField] private Transform[] CounterPos;
    [SerializeField] private Transform[] WaitPos;
    [SerializeField] private Transform[] leavePos;

    [Header("Variables")]
    [SerializeField] private float waitTime = 10f;
    [SerializeField] private float customerSpeed = 1f;

    private bool customerActive;
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
        if (!customerActive)
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
    }

    private IEnumerator CustomerSpawn()
    {
        customerActive = true;
        yield return new WaitForSeconds(waitTime);

        InitializeCustomer();
        // move customer to counter pos
        foreach(Transform pos in CounterPos)
        {
            while (customerObj.transform.position != pos.position)
            {
                MoveCustomer(customerObj.transform, pos);
                yield return null;
            }
        }

    }

    private void MoveCustomer(Transform customer, Transform endPoint)
    {
        if (customer.position != endPoint.position)
        {
            customer.localPosition = Vector3.MoveTowards(customer.transform.localPosition, endPoint.position, customerSpeed * Time.deltaTime);
        }
    }
}
