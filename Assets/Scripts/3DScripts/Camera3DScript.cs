using UnityEngine;
using System.Collections;

public class Camera3DScript : MonoBehaviour
{
	public GameObject player3D;
	public float rotateSpeed = 5;

	Vector3 offset;

	void Start ()
	{
		offset = player3D.transform.position - transform.position;
	}

	void LateUpdate ()
	{
		float horizontal = Input.GetAxis ("Mouse X") * rotateSpeed;
		player3D.transform.Rotate (0, horizontal, 0);
		float desiredAngle = player3D.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);
		transform.position = player3D.transform.position - (rotation * offset);
		//transform.LookAt (player3D.transform);
	}
}