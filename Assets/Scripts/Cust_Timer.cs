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
    private bool angry;
    private Coroutine coroutine;
    private Coroutine barProgressCoroutine;

    public void StartTimer()
    {
        bar = transform.parent.GetComponent<CustomerListV2>().patienceBar;
        bar.gameObject.SetActive(true);
        bar.color = calmColour;
        coroutine = StartCoroutine(TimeMeterDrain(time));
    }

    private void FixedUpdate()
    {
        if (bar == null)
            return;

        else if (!angry && bar.fillAmount < 0.5)
            BecomeAngry();

        else if (bar.fillAmount < 0.01f)
            Kill();

    }
    private IEnumerator TimeMeterDrain(float time)
    {
        yield return barProgressCoroutine = StartCoroutine(BarProgression.Progress(bar, false, time, bar.fillAmount));
        StopTimer();
    }

    public void StopTimer()
    {
        StopCoroutine(coroutine);
        bar.gameObject.SetActive(false);
        bar.fillAmount = 1f;
        Destroy(this);
    }

    public void BecomeAngry()
    {
        angry = true;
        StopCoroutine(coroutine);
        StopCoroutine(barProgressCoroutine);
        gameObject.GetComponent<Customer>().BecomeAngry();
        bar.color = angryColour;
        coroutine = StartCoroutine(TimeMeterDrain(bar.fillAmount * time / angerFactor));
    }

    private void Kill()
    {
        Death deathScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Death>();
        deathScript.StartCoroutine(deathScript.Die());
    }
}