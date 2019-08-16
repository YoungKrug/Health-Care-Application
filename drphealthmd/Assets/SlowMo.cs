using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour
{
    [Range(0.1f,0.99f)]
    public float slowMotionSpeed;
    public float slowMoTime;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(SlowMotion());
    }

    IEnumerator SlowMotion()
    {
        Time.timeScale = slowMotionSpeed;
        yield return new WaitForSeconds(slowMoTime);
        Time.timeScale = 1f;
    }
}
