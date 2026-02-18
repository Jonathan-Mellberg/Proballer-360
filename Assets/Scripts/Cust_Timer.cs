using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cust_Timer : MonoBehaviour
{
    [SerializeField] private float time = 120;
    [SerializeField] int angerFactor = 2;
    private Image bar;

    private float startTime;
    private float timeDrain = 1;
    private bool iritated = false;

    public void StartTimer()
    {
        bar = transform.parent.GetComponent<CustomerListV2>().patienceBar;
        //bar = GameObject.Find("PatienceBar").GetComponent<Image>();
        startTime = time;

        if (iritated)
        {
            timeDrain *= 2;
        }
        
        StartCoroutine(TimeMeterDrain());
    }
    private IEnumerator TimeMeterDrain()
    {
        while (time > 0)
        {            
            bar.fillAmount =  time / startTime;
            time -= timeDrain;
            yield return new WaitForSeconds(1);
        }

       StopTimer();
    }

    public void StopTimer()
    {
        StopCoroutine(TimeMeterDrain());
        bar.fillAmount = 1f;
        Destroy(this);
    }
}