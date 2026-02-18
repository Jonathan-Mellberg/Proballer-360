using UnityEngine;

public class coffeeMaking : Interaction
{
    public GameObject coffeeStream;
    public GameObject coffee;

    public float growSpeed = 0.5f;
    float minScale = 0.01f;
    float maxScale = 1f;

    [HideInInspector] public bool canFill = false;

    void Start()
    {
        coffee.transform.localScale = new Vector3(1f, minScale, 1f);
        coffeeStream.SetActive(false);
        coffee.SetActive(false);
    }


    public override void Interact()
    {
        base.Interact();
        Debug.Log("interact");
        if (canFill)
        {
            FillCup();
        }
    }

    private void FillCup()
    {
        coffee.SetActive(true);
        while (Input.GetKey(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.E) && coffee.transform.localScale.y == maxScale)
            {
                Debug.Log("full");
                coffeeStream.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("grow");
                coffeeStream.SetActive(true);
                float newScale = Mathf.MoveTowards(coffee.transform.localScale.y, maxScale, growSpeed * Time.deltaTime);

                coffee.transform.localScale = new Vector3(1f, newScale, 1f);
            }
            else
                coffeeStream.SetActive(false);
        }
        
    }
}
