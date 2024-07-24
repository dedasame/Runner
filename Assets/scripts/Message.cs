using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public float fadeTime;
    public TextMeshProUGUI fadeText;
    public float alphaValue;
    public float fps;
    // Start is called before the first frame update
    void Start()
    {
        fadeText = GetComponent<TextMeshProUGUI>();
        fps = 1 / fadeTime;
        alphaValue = fadeText.color.a;

    }

    // Update is called once per frame
    void Update()
    {
        if(fadeTime >0)
        {
            fadeTime -= Time.deltaTime;
            alphaValue -= fps * Time.deltaTime;
            fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, alphaValue); //fadeTime or alphaValue
        }
    }
}
