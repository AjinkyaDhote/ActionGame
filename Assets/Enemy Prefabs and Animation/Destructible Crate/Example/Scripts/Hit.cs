using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	public GameObject DestroyedObject;


	
	void OnCollisionEnter( Collision collision ) {
		if( collision.relativeVelocity.magnitude > 5f) {
		DestroyIt();
		}
	}
	
		void DestroyIt(){
		if(DestroyedObject) {
			Instantiate(DestroyedObject, transform.position, transform.rotation);
		}
		Destroy(gameObject);

	}
}