using System.Collections;
using UnityEngine;

public class coffeeMaking : Interaction
{
    public GameObject coffeeStream;
    public GameObject coffee;

    public float growSpeed = 0.5f; // speed of interpolation

    float fillAmount = 0f; // 0 → 1

    Vector3 minScale = new Vector3(0.73f, 0.01f, 0.73f);
    Vector3 maxScale = Vector3.one;

    [HideInInspector] public bool canFill = false;

    void Start()
    {
        coffee.transform.localScale = minScale;
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

        while (Input.GetKey(KeyCode.E))
        {
            if (fillAmount >= 1f)
            {
                coffeeStream.SetActive(false);
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
