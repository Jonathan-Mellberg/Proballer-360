using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cust_Timer : MonoBehaviour
{
    public float time = 120;

    [SerializeField] Image bar;
    [SerializeField] int angerFactor = 2; 

    private float startTime;
    private float timeDrain = 1;
    private bool iritated = false;

    private void Start()
    {
        bar = GameObject.Find("SuperSmexBarrrr").GetComponent<Image>();
        StartTimer();
    }

    public void StartTimer()
    {
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