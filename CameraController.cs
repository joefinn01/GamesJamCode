using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mMainCamera;

    private Transform mCameraTransform;

    private Vector3 mForwardVector;
    private Vector3 mRightVector;

    private Vector3 mPreviousMousePosition;

    private int speedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        speedMultiplier = 250;
        mForwardVector = new Vector3(0, 0, 1);
        mRightVector = new Vector3(1, 0, 0);

        mMainCamera = Camera.main;

        mCameraTransform = mMainCamera.GetComponent<Transform>();

        mPreviousMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        WASDCameraControl();
        MouseWheelCameraControl();
        CameraRotation();
        CameraZoom();

    }

    void CameraRotation()
    {
        //Rotation with mouse

        if (Input.GetMouseButtonDown(1)) //Ensuring there isn't a jump when mouse button first pressed
        {
            mPreviousMousePosition = Input.mousePosition;
        }


        if (Input.GetMouseButton(1))
        {
            float angleY = (mPreviousMousePosition.x - Input.mousePosition.x) * Time.deltaTime * 20.0f;
            float angleX = (Input.mousePosition.y - mPreviousMousePosition.y) * Time.deltaTime * 20.0f;

            mCameraTransform.rotation = Quaternion.Euler(mCameraTransform.eulerAngles.x + angleX, mCameraTransform.eulerAngles.y + angleY, 0.0f);

            //Get new forward and right vectors
            mForwardVector = mCameraTransform.forward;
            mRightVector = mCameraTransform.right;

            mPreviousMousePosition = Input.mousePosition;
        }
    }

    void WASDCameraControl()
    {
        if (Input.GetKey(KeyCode.W)) //Forward movement
        {
            mCameraTransform.position += mForwardVector * Time.deltaTime * speedMultiplier;
        }

        if (Input.GetKey(KeyCode.S)) //Backward movement
        {
            mCameraTransform.position -= mForwardVector * Time.deltaTime * speedMultiplier;
        }

        if (Input.GetKey(KeyCode.D)) //Right movement
        {
            mCameraTransform.position += mRightVector * Time.deltaTime * speedMultiplier;
        }

        if (Input.GetKey(KeyCode.A)) //Left movement
        {
            mCameraTransform.position -= mRightVector * Time.deltaTime * speedMultiplier;
        }
    }

    void MouseWheelCameraControl()
    {
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.LeftAlt)) //Ensuring there isn't a jump when scroll wheel first pressed
        {
            mPreviousMousePosition = Input.mousePosition;
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            mCameraTransform.position += new Vector3(0, -1, 0) * (Input.mousePosition.y - mPreviousMousePosition.y) * Time.deltaTime * 205;

            mPreviousMousePosition = Input.mousePosition;
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            speedMultiplier = 500;
        }
        else
        {
            speedMultiplier = 250;
            if (Input.GetMouseButton(2))
            {
                mCameraTransform.position += (mRightVector * (Input.mousePosition.x - mPreviousMousePosition.x)
                    + mForwardVector * (Input.mousePosition.y - mPreviousMousePosition.y)) * Time.deltaTime * 1.25f;

                mPreviousMousePosition = Input.mousePosition;
            }
        }

    }

    void CameraZoom()
    {
        mCameraTransform.position += mCameraTransform.forward * Input.GetAxis("Mouse ScrollWheel") * 150.0f * Time.deltaTime;
    }
}