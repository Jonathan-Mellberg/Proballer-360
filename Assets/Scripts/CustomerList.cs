using System;
using UnityEngine;
public class CustomerList : MonoBehaviour
{
    public static CustomerList instance { get; private set; }

    public int customerIndex;
    //public GameObject[] charachters;
    public Customer[] customers;

    // Singleton Class
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else if (instance != this)
            Destroy(gameObject);
    }

    public class Customer
    {
        GameObject avatar;
        string username;
        // order, WIP
    }
    

}