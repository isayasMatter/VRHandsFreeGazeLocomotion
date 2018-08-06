using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiltController : MonoBehaviour {

    public Camera playerCam;
    public float speed;
    public float rollMinLeft;
    public float rollMinRight;
    public float pitchMin;

    private float headRoll;
    private float minRightRoll;
    private float minLeftRoll;

    private float headPitch;
    private float minBackPitch;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        minRightRoll = 360 - rollMinRight;
        minLeftRoll = rollMinLeft;

        minBackPitch = 360 - pitchMin;

        rb=GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        headPitch = playerCam.transform.rotation.eulerAngles.x;
        headRoll = playerCam.transform.rotation.eulerAngles.z;
        
        if (headRoll > 180 && headRoll < minRightRoll)
        {
            Vector3 newPos = playerCam.transform.right * speed * Time.deltaTime;
            newPos.y = transform.position.y;
            // move to the right            
           rb.MovePosition(transform.position + newPos);
        }

        if(headRoll <= 180 && headRoll > minLeftRoll)
        {
            Vector3 newPos = playerCam.transform.right * speed * Time.deltaTime;
            newPos.y = transform.position.y;
            // move to the left
            rb.MovePosition(transform.position - newPos);
        }

        if(headPitch > 180 && headPitch < minBackPitch)
        {
            Vector3 newPos = playerCam.transform.forward * speed * Time.deltaTime;
            newPos.y = transform.position.y;
            rb.MovePosition(transform.position - newPos);
        }

        if(headPitch < 180)
        {
            //speed = Mathf.Lerp(0,100,(1-(headPitch/180)));
            Vector3 newPos = playerCam.transform.forward * speed * Time.deltaTime;
            newPos.y = transform.position.y;
            rb.MovePosition(transform.position + newPos);
        }

        
	}
}
