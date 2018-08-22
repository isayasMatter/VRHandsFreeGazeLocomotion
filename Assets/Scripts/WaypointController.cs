using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WaypointController : MonoBehaviour {
    List<Vector3> waypointPositions = new List<Vector3>();     
    int counter = 0;

	// Use this for initialization
	void Start () {
        waypointPositions.Add(new Vector3(153,4,244));
        waypointPositions.Add(new Vector3(181,4,237));
        waypointPositions.Add(new Vector3(116,4,293));
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            transform.position = waypointPositions[counter];
            counter++;
            if(counter == waypointPositions.Count){
                counter = 0;
            }
        }
        
    }
}
