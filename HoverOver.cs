using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverOver : MonoBehaviour
{
    //ADD ANIMAL SCRIPT REFERENCE
    private bool mHoverOver;

    private static GameObject mTextObject;

    private Canvas mCanvas;

    private bool mPlaced;

    private GameObject mPanel;
    private GameObject mText;

    public void SetPlaced(bool placed)
    {
        mPlaced = placed;
    }

    // Start is called before the first frame update
    void Start()
    {
        mHoverOver = false;
        mPlaced = false;

        mCanvas = FindObjectOfType<Canvas>();
        //mPanel = GameObject.Find("HappinessPanel");
        //mText = GameObject.Find("HappinessText");

        //if (mPanel == null)
        //{
        //    Debug.Log("Panel null");
        //}
        //if (mText == null)
        //{
        //    Debug.Log("Text null");
        //}

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        mHoverOver = true;

        if (mPlaced)
        {
            mPanel = new GameObject();
            mPanel.AddComponent<CanvasRenderer>();
            mPanel.AddComponent<RectTransform>();

            if (mPanel == null)
            {
                Debug.Log("NULL");
            }
            Text text = mPanel.AddComponent<Text>();
            text.text = "Happiness: " + GetComponentInChildren<HappinessCalc>().happiness;
            text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text.color = Color.black;
            mPanel.transform.SetParent(mCanvas.transform);
        }


        //if (mPlaced)
        //{
        //    mPanel.SetActive(true);
        //    mText.SetActive(true);
        //}


        //mText.GetComponent<Text>().text = "Happiness: 1000";
    }

    void OnMouseExit()
    {
        mHoverOver = false;

        Destroy(mPanel);

        mPanel = null;
        mText = null;

        //mPanel.SetActive(false);
        //mText.SetActive(false);
    }

    void OnGUI()
    {
        if (mHoverOver && mPlaced && mPanel != null)
        {
            //GUI.Box(new Rect(Input.mousePosition.x, Camera.main.pixelHeight - Input.mousePosition.y , 150.0f, 25.0f), "Happiness: 1000");

            mPanel.GetComponent<RectTransform>().localPosition = new Vector3(Input.mousePosition.x - (Camera.main.pixelWidth / 2.0f), Input.mousePosition.y - (Camera.main.pixelHeight / 2.0f), 0.0f);
        }
    }
}