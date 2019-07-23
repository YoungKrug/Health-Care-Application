using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour {
     float rotateSpeed = 100;
	
     void OnMouseDrag()
     {
          float X = Input.GetAxis("Mouse X") * rotateSpeed * Mathf.Deg2Rad;
          transform.Rotate(Vector3.up, -X);
     }
}
