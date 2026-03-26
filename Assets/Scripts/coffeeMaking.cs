using System.Collections;
using UnityEngine;

public class coffeeMaking : Interaction
{
    [HideInInspector] public GameObject coffee = null; // gets set by takecup
    [HideInInspector] public bool canFill = false;

    [SerializeField] private GameObject coffeeStream; // set as not activ and match its material to what the machine 
    [SerializeField] private float growSpeed = 0.5f; // speed of interpolation
    private float fillAmount = 0f; // 0 → 1

    private Vector3 minScale = new Vector3(0.73f, 0.01f, 0.73f); // makes sure the coffee doesnt clip through the cup
    private Vector3 maxScale = Vector3.one;

    void Start()
    {
        coffeeStream.SetActive(false);
    }

    public override void Interact()
    {
        base.Interact();
        if (canFill)
        {
            StartCoroutine(FillCup());
        }
    }

    private IEnumerator FillCup()
    {
        coffee.SetActive(true);
        // match coffee color with coffee flow
        if(coffee.GetComponent<MeshRenderer>().material != coffeeStream.GetComponent<MeshRenderer>().material)
            coffee.GetComponent<MeshRenderer>().material = coffeeStream.GetComponent<MeshRenderer>().material;

        while (Input.GetKey(KeyCode.E))
        {
            if (fillAmount >= 1f)
            {
                coffeeStream.SetActive(false);
                coffee.transform.parent.Find("lid").gameObject.SetActive(true);
            }
            else
            {
                coffeeStream.SetActive(true);

                // Increase normalized fill
                fillAmount = Mathf.MoveTowards(fillAmount, 1f, growSpeed * Time.deltaTime);

                // Interpolate all axes
                coffee.transform.localScale = Vector3.Lerp(minScale, maxScale, fillAmount);
            }

            yield return null;
        }

        coffeeStream.SetActive(false);
    }
}
