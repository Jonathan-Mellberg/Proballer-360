using UnityEngine;

public class coffeeMaking : MonoBehaviour
{
    public GameObject coffeeStream;

    public float growSpeed = 0.5f;
    float minScale = 0.01f;
    float maxScale = 1f;

    void Start()
    {
        coffeeStream.SetActive(false);
        transform.localScale = new Vector3(1f, minScale, 1f);
        Debug.Log(transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && transform.localScale.y == maxScale)
        {
            coffeeStream.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            coffeeStream.SetActive(true);
            float newScale = Mathf.MoveTowards(transform.localScale.y, maxScale, growSpeed * Time.deltaTime);

            transform.localScale = new Vector3(1f, newScale, 1f);
        }
        else
            coffeeStream.SetActive(false);
    }
}
