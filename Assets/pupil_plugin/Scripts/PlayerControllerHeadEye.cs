using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerHeadEye : MonoBehaviour {
	public float rotationSpeed = 50.0f;
	
	public float maxMovementSpeed = 50.0f;
	
	public float minMovementSpeed = 1.0f;

	public float movementSpeed = 5.0f;
	public float leftTurnThreshold = 0.4f;

	public float rightTurnThreshold = 0.6f;

	public float backWardMovementThreshold = 0.45f;

	public float forwardMovementThreshold = 0.55f;

	public Slider sliderx, slidery;

	public Image FillX, FillY;

	public Camera mainCamera;

 	Rigidbody rb;

	Vector3 newPosition;
	
	private List<float> gazeArrayX = new List<float>();
	float gazeAverageX = 0.0F;

	private List<float> gazeArrayY = new List<float>();
	float gazeAverageY = 0.0f;

	public float frameCounter = 20;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		if (PupilTools.IsGazing)
		{
			if (PupilTools.CalibrationMode == Calibration.Mode._2D)
			{
				Vector2 positions = PupilData._2D.GazePosition;

				Debug.Log(PupilData._2D.GazePosition);	

				smoothGaze(PupilData._2D.GazePosition.x, PupilData._2D.GazePosition.y);			

				//checkRotation(PupilData._2D.GazePosition.x, PupilData._2D.GazePosition.y);
				sliderx.value = PupilData._2D.GazePosition.x;
				slidery.value = PupilData._2D.GazePosition.y;
			}
			else if (PupilTools.CalibrationMode == Calibration.Mode._3D)
			{
				Debug.Log(PupilData._3D.GazePosition);			
			}
		} 
	}

	void smoothGaze(float x, float y){
		gazeAverageY = 0f;
		gazeAverageX = 0f;

		gazeArrayY.Add(y);
		gazeArrayX.Add(x);

		if (gazeArrayY.Count >= frameCounter) {
			gazeArrayY.RemoveAt(0);
		}
		if (gazeArrayX.Count >= frameCounter) {
			gazeArrayX.RemoveAt(0);
		}

		for(int j = 0; j < gazeArrayY.Count; j++) {
			gazeAverageY += gazeArrayY[j];
		}
		
		for(int i = 0; i < gazeArrayX.Count; i++) {
			gazeAverageX += gazeArrayX[i];
		}

		gazeAverageY /= gazeArrayY.Count;
		gazeAverageX /= gazeArrayX.Count;

		Debug.Log(x + ", " + y + ", " + gazeAverageX + ", " + gazeAverageY);
		checkRotation(gazeAverageX, gazeAverageY);
		
	}

	void checkRotation(float x, float y){			

		if(y > forwardMovementThreshold){		
			//movementSpeed = Mathf.Lerp(minMovementSpeed, maxMovementSpeed, ((y-forwardMovementThreshold)*2.5f));
			newPosition = mainCamera.transform.forward * Time.deltaTime * movementSpeed;
			rb.MovePosition(transform.position + newPosition);
			//transform.position += transform.forward * Time.deltaTime * movementSpeed;
			FillY.color = Color.green;
		}

		if(y < backWardMovementThreshold){
			//movementSpeed = Mathf.Lerp(minMovementSpeed, maxMovementSpeed, ((backWardMovementThreshold-y)*2.5f));		
			newPosition = mainCamera.transform.forward * Time.deltaTime * movementSpeed * -1;
			rb.MovePosition(transform.position + newPosition);
			//transform.position += transform.forward * Time.deltaTime * movementSpeed * -1;
			FillY.color = Color.red;
		}

		if (y > backWardMovementThreshold && y < forwardMovementThreshold){
			FillY.color = Color.white;			
		}

	}
}
