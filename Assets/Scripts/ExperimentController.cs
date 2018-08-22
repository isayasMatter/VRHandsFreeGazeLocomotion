using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentController : MonoBehaviour {
	private PlayerControllerHeadEye gazeScript;
	private tiltController tiltScript;

	public Slider slider;
	public Text menuText;
	// Use this for initialization
	void Start () {
		tiltScript = GetComponent<tiltController>();
		gazeScript = GetComponent<PlayerControllerHeadEye>();	
		slider.enabled =  false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("t")){
			menuText.enabled = false;			
			gazeScript.enabled = false;
			tiltScript.enabled = true;	
			slider.enabled = false;		
		}
		if(Input.GetKey("g")){
			menuText.enabled = false;	
			tiltScript.enabled = false;
			gazeScript.enabled = true;
			slider.enabled = true;
		}
	}
}
