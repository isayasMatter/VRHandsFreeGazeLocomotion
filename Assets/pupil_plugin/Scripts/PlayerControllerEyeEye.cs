using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEyeEye : MonoBehaviour {
	public float rotationSpeed = 50.0f;
	
	public float maxMovementSpeed = 50.0f;
	
	public float minMovementSpeed = 1.0f;

	float movementSpeed = 5.0f;
	public float leftTurnThreshold = 0.4f;

	public float rightTurnThreshold = 0.6f;

	public float backWardMovementThreshold = 0.45f;

	public float forwardMovementThreshold = 0.55f;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (PupilTools.IsGazing)
		{
			if (PupilTools.CalibrationMode == Calibration.Mode._2D)
			{
				Vector2 positions = PupilData._2D.GazePosition;

				Debug.Log(PupilData._2D.GazePosition);				

				checkRotation(PupilData._2D.GazePosition.x, PupilData._2D.GazePosition.y);
			}
			else if (PupilTools.CalibrationMode == Calibration.Mode._3D)
			{
				Debug.Log(PupilData._3D.GazePosition);			
			}
		} 
	}

	void checkRotation(float x, float y){		

		if(x < 0.5){
			Debug.Log("Rotate left");
			transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * -1);
		}
		if (x > 0.5){
			Debug.Log("Rotate right");
			transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
		}
		// if((x >= leftTurnThreshold) && (x<=rightTurnThreshold)){
		// 	Debug.Log("Do not Rotate");
		// }

		if(y>forwardMovementThreshold){		
			movementSpeed = Mathf.Lerp(minMovementSpeed, maxMovementSpeed, ((y-forwardMovementThreshold)*2.5f));
			transform.position += Camera.main.transform.forward * Time.deltaTime * movementSpeed;
		}

		if(y<backWardMovementThreshold){
			movementSpeed = Mathf.Lerp(minMovementSpeed, maxMovementSpeed, ((backWardMovementThreshold-y)*2.5f));		
			transform.position += Camera.main.transform.forward * Time.deltaTime * movementSpeed * -1;
		}

	}
}
