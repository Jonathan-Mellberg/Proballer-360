using System.Collections;
using UnityEngine;
using UnityEngine.UI;

static class BarProgression
{
    public static IEnumerator Progress(Image bar, bool fill, float progressTime)
    {
        int startFill = fill ? 0 : 1;
        int endFill = fill ? 1 : 0;
        bar.fillAmount = startFill;

        float time = 0f;
        while (time < progressTime)
        {
            bar.fillAmount = Mathf.Lerp(startFill, endFill, time / progressTime);
            time += Time.deltaTime;

            yield return null;
        }

        bar.fillAmount = endFill;
    }
}