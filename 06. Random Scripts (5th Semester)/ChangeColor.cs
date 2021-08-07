using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        StartCoroutine(ChangeColors());
    }

    IEnumerator ChangeColors()
    {
        yield return new WaitForSeconds(2f);

        while(true)
        {
            rend.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            yield return new WaitForSeconds(0.35f);
        }
    }
}
