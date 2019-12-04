using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPlacement : MonoBehaviour
{
    public Camera mMainCamera;

    public GameObject mPlaceableObject;
    public GameObject mPlacedObjectGhost;
    float mPlacementDistance;
    private bool mPlaceable;

    public void SetPlaceable(bool isPlaceable)
    {
        mPlaceable = isPlaceable;
    }

    // Start is called before the first frame update
    void Start()
    {
        mPlacementDistance = 10.0f;

        Ray testRay = mMainCamera.ScreenPointToRay(Input.mousePosition);

        mPlaceable = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Update placement distance
        mPlacementDistance += Input.GetAxis("Mouse ScrollWheel") * 10.0f;

        //Draw ray from cursor position
        Ray ray = mMainCamera.ScreenPointToRay(Input.mousePosition);

        if (mPlacedObjectGhost == null && mPlaceableObject != null) //If object has been placed create new object
        {
            mPlacedObjectGhost = Instantiate(mPlaceableObject, ray.GetPoint(mPlacementDistance), new Quaternion(0, 0, 0, 1));
        }
        else
        {
            if(mPlacedObjectGhost != null)
                mPlacedObjectGhost.GetComponent<Transform>().position = ray.GetPoint(mPlacementDistance);

            //if (mPlaceable)
            //{
            //    mPlacedObjectGhost.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            //}
            //else
            //{
            //    mPlacedObjectGhost.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            //}


            if (mPlaceableObject != null && Input.GetMouseButtonDown(0) && mPlaceable && GetComponent<UiController>().mHappiness >= 50)  //If clicked
            {
                if (mPlacedObjectGhost.tag == "Elephant" || mPlacedObjectGhost.tag == "Tiger")
                {
                    mPlacedObjectGhost.GetComponent<HoverOver>().SetPlaced(true);
                    mPlacedObjectGhost.GetComponent<AnimalAI>().SetPlaced(true);
                }
                else
                {
                    mPlacedObjectGhost.GetComponent<StaticObjects>().mPlaced = true;
                }

                mPlacedObjectGhost = null;

                GetComponent<UiController>().mHappiness -= 50;
            }
        }
    }
}