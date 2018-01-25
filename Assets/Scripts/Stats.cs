using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    private Image content;

    private float lerpSpeed = 3f;
    public float currentValue;
    public float currentFill;

    public float myMaxValue { get; set; }

    public float myCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > myMaxValue)
            {
                currentValue = myMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / myMaxValue;
        }
    }


    // Use this for initialization
    void Start () {
        content = GetComponent<Image>();
        
        currentFill = content.fillAmount;
	}
	
	// Update is called once per frame
	void Update () {

        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }

    /// <summary>
    /// Initialize health for the character
    /// </summary>
    /// <param name="MaxValue"></param>
    public void Initialize( float MaxValue)
    {
        myMaxValue = MaxValue;
        myCurrentValue = MaxValue;
       

    }

}
