using UnityEngine;

 
public class NegateTracking : MonoBehaviour
{
    void Update()
    {		
        UnityEngine.XR.InputTracking.disablePositionalTracking = true;
    }
}