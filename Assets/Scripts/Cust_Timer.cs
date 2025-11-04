using UnityEngine;
using System.Collections;

public class Cust_Timer : MonoBehaviour
{
    double time = 120;
    double timeDrain = 1;
    bool Iritated = false;
    private void Awake()
    {
        if (Iritated)
        {
            timeDrain = timeDrain * 1.2;
        }
        TimeMeterDrain(1f);
    }
    private IEnumerator TimeMeterDrain(float wait)
    {
        while (time >= 0)
        {
            yield return new WaitForSeconds(wait);
            time = time - timeDrain;
        }
    }
}
