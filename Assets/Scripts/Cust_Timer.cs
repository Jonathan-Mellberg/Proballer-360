using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cust_Timer : MonoBehaviour
{
    [SerializeField] private float time = 180;
    [SerializeField] int angerFactor = 2;
    [SerializeField] Color calmColour;
    [SerializeField] Color angryColour;
    private Image bar;
    private float elapsedTime;

    public void StartTimer()
    {
        elapsedTime = time;
        bar = transform.parent.GetComponent<CustomerListV2>().patienceBar;
        bar.gameObject.SetActive(true);
        bar.color = calmColour;
        StartCoroutine(TimeMeterDrain());
    }

    private void FixedUpdate()
    {
        if (bar.enabled && bar.fillAmount < 0.5 && bar.color == calmColour)
            bar.color = angryColour;
    }
    private IEnumerator TimeMeterDrain()
    {
        yield return StartCoroutine(BarProgression.Progress(bar, false, elapsedTime));
        StopTimer();
    }

    public void StopTimer()
    {
        StopCoroutine(TimeMeterDrain());
        bar.gameObject.SetActive(false);
        bar.fillAmount = 1f;
        Destroy(this);
    }

    public void BecomeAngry()
    {
        StopCoroutine(TimeMeterDrain());

        elapsedTime = bar.fillAmount * time * 1 / angerFactor;
        StartCoroutine(TimeMeterDrain());
    }
}