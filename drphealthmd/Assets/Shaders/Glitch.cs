using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch : MonoBehaviour
{
    public float ColorLerp = 0.2f;
    float ticks = 0.0f;

    private float glitchChance = 0.1f;

    private Renderer glitchRenderer;
    private WaitForSeconds glitchLoopWait = new WaitForSeconds(.1f);
    private WaitForSeconds glitchDuration = new WaitForSeconds(.1f);

    private void Awake()
    {
        glitchRenderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {

        glitchRenderer.material.SetFloat("_Amount", 0f);
        glitchRenderer.material.SetFloat("_CutoutThresh", 0f);
        glitchRenderer.material.SetFloat("_Distance", 105f);

        //StartCoroutine(ColorRotate());
        while (true)
        {
            float glitchTest = Random.Range(0f, 1f);

            if (glitchTest <= glitchChance)
            {
                //StartCoroutine(_Glitch());
            }
            yield return glitchLoopWait;
        }
    }

    private void Update()
    {
        //ColorRotate();
    }

    IEnumerator ColorRotate()
    {
        //while (true)
        {
            Color col = glitchRenderer.material.color;
            Color.Lerp(col, Color.green, ColorLerp);
            if (ticks > ColorLerp)
            {
                Color.Lerp(col, Color.red, ColorLerp);
            }
            else if (ticks > (ColorLerp * 2))
            {
                Color.Lerp(col, Color.magenta, ColorLerp);
            }
            else if (ticks > (ColorLerp * 3))
            {
                ticks = 0;
            }
            ticks += Time.deltaTime;
            col.a = 0f;
            glitchRenderer.material.color = col;
        }
        yield return new WaitForSeconds(.01f);
    }

    IEnumerator _Glitch()
    {
        glitchDuration = new WaitForSeconds(Random.Range(0.05f, 0.25f));
        glitchRenderer.material.SetFloat("_Amount", .2f);
        glitchRenderer.material.SetFloat("_CutoutThresh", .29f);
        glitchRenderer.material.SetFloat("_Amplitude", Random.Range(5, 30));
        glitchRenderer.material.SetFloat("_Speed", Random.Range(20, 40));
        yield return glitchDuration;
        glitchRenderer.material.SetFloat("_Amount", 0f);
        glitchRenderer.material.SetFloat("_CutoutThresh", 0f);
    }
}
