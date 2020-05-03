using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActiveSign : MonoBehaviour
{
    public Text sign;
    public float fadeSpeed = 5f;
    public bool isActive;
    public GameObject cemetary;
    // Start is called before the first frame update
    void Start()
    {
        sign = cemetary.GetComponentInChildren<Text>();
        sign.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        ColorChange();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isActive = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isActive = false;
    }
    private void ColorChange()
    {
        if (isActive)
            sign.color = Color.Lerp(sign.color, Color.white, fadeSpeed * Time.deltaTime);
        else
            sign.color = Color.Lerp(sign.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
}
