using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text mHappyText;

    public int mHappiness;
    public int mChangeInHappiness;

    private float mTimer;

    // Start is called before the first frame update
    void Start()
    {
        mHappiness = 1000;
        mTimer = 0;
        mHappyText.text = mHappiness.ToString() + " + " + mChangeInHappiness.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (mChangeInHappiness >= 0)
        {
            mHappyText.text = "Happiness : " + mHappiness.ToString() + "<color=green> + " + mChangeInHappiness.ToString() + "</color>";
        }
        else
        {
            mHappyText.text = "Happiness : " + mHappiness.ToString() + "<color=red> - " + mChangeInHappiness.ToString().Substring(1) + "</color>";
        }

        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        int tempCount=0;
        foreach(GameObject o in obj)
        {
            if(((GameObject)o).tag == "moodSphere")
            {
                if(((GameObject)o).GetComponent<HappinessCalc>().onGround)
                {
                    tempCount += ((GameObject)o).GetComponent<HappinessCalc>().happiness;
                }

            }
        }
        mChangeInHappiness = tempCount;

        mTimer += Time.deltaTime;
        if(mTimer>=2.0f)
        {
            mHappiness += mChangeInHappiness;
            mTimer = 0;
        }
    }
}