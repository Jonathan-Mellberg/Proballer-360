using UnityEngine;

public class CustomerListV2 : MonoBehaviour
{
    public static CustomerListV2 instance { get; private set; }

    [HideInInspector] public int customerIndex;
    public GameObject[] charachters;
    public Customer[] customers;

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
}
