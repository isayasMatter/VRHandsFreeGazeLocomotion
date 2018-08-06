using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixationDetection : MonoBehaviour 
{
	void Start () 
	{
		PupilTools.OnConnected += StartPupilSubscription;
		PupilTools.OnDisconnecting += StopPupilSubscription;
	}

	void StartPupilSubscription()
	{
		PupilTools.CalibrationMode = Calibration.Mode._2D;

		PupilTools.SubscribeTo ("pupil.");
	}

	void StopPupilSubscription()
	{
		PupilTools.UnSubscribeFrom ("pupil.");
	}

	

	void Update()
	{
		if (PupilTools.IsGazing)
		{
			if (PupilTools.CalibrationMode == Calibration.Mode._2D)
			{
				Vector2 positions = PupilData._2D.GazePosition;

				Debug.Log(PupilData._2D.GazePosition);
				
			}
			else if (PupilTools.CalibrationMode == Calibration.Mode._3D)
			{
				Debug.Log(PupilData._3D.GazePosition);				
			}
		} 
		
	}


	void OnDisable()
	{
		PupilTools.OnConnected -= StartPupilSubscription;
		PupilTools.OnDisconnecting -= StopPupilSubscription;

		
	}
}
