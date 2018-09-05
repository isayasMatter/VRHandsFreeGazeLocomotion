using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;

public class ExperimentController : MonoBehaviour {
	private PlayerControllerHeadEye gazeScript;
	private tiltController tiltScript;

	public Camera mainCamera;

	public Slider slider;
	public Text menuText;

	List<Vector3> positions;
	List<Vector3> rotations;

	List<Vector2> gazePositions;

	List<string> timeStamps;
	// Use this for initialization
	void Start () {
		tiltScript = GetComponent<tiltController>();
		gazeScript = GetComponent<PlayerControllerHeadEye>();	
		slider.gameObject.SetActive(false);

		positions = new List<Vector3>();
		rotations = new List<Vector3>();
		timeStamps = new List<string>();
		gazePositions = new List<Vector2>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("t")){
			menuText.enabled = false;			
			gazeScript.enabled = false;
			tiltScript.enabled = true;	
			slider.gameObject.SetActive(false);		
		}
		if(Input.GetKey("g")){
			menuText.enabled = false;	
			tiltScript.enabled = false;
			gazeScript.enabled = true;
			//slider.gameObject.SetActive(true);
		}
	}

	void LateUpdate(){
		positions.Add(transform.position);
		rotations.Add(mainCamera.transform.rotation.eulerAngles);
		timeStamps.Add(Time.time.ToString());

		if (PupilTools.IsGazing)
		{
			if (PupilTools.CalibrationMode == Calibration.Mode._2D)
			{
				Vector2 gazePosition = PupilData._2D.GazePosition;
				gazePositions.Add(gazePosition);								
				
			}			
		} 
	}

	void savePositions(){
		System.DateTime theTime = System.DateTime.Now;
		string fileName = theTime.ToString("yyyy-MM-dd\\THH:mm:ss\\Z");
		fileName = fileName.Replace(":","_");
		StringBuilder sb = new StringBuilder();
        sb.Append(fileName);        
        sb.Append(".csv");
        string fullFileName = sb.ToString();

        StreamWriter writer = new StreamWriter(fullFileName);

        sb = new StringBuilder();

        sb.Append("time,pos-x,pos-y,pos-z,");
        sb.Append("rot-x,rot-y,rot-z,");
		sb.Append("gaze-x,gaze-y\n");

		long count = positions.Count;

		for(int i=0; i<count; i++){
			sb.Append(timeStamps[i]).Append(",");
			sb.Append(positions[i].x).Append(",").Append(positions[i].y).Append(",").Append(positions[i].z).Append(",");				
			sb.Append(rotations[i].x).Append(",").Append(rotations[i].y).Append(",").Append(rotations[i].z).Append(",");
			sb.Append(gazePositions[i].x).Append(",").Append(gazePositions[i].y).Append("\n");
		}

        writer.Write(sb);
        writer.Close();
	}

	void OnDisable(){
		savePositions();
	}

}
