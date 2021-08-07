using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorReveal : MonoBehaviour
{
    public float r;
    public float g;
    public float b;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FakeKey"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0.5f, 1f), Random.Range(0f, 0.5f), Random.Range(0.5f, 1f));
        }

        if (other.CompareTag("GBrokenKey") && gameObject.CompareTag("GreenReveal"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = new Color(0.4270309f, 0.7924528f, 0.265397f);
            other.tag = "GKey";
        }

        if (other.CompareTag("PBrokenKey") && gameObject.CompareTag("PurpleReveal"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = new Color(0.4514024f, 0.3272072f, 0.5377358f);
            other.tag = "PKey";
        }

        if (other.CompareTag("YBrokenKey") && gameObject.CompareTag("YellowReveal"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = new Color(0.9618509f, 1f, 0.0518868f);
            other.tag = "YKey";
        }
    }
}
