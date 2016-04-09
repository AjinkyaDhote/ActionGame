using UnityEngine;
using System.Collections;

public class EnemyThrow : MonoBehaviour {
    public GameObject crate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
	
	}

    void thrower()
        {
        Gameobject crate = GameObject.FindWithTag("Crate");
        crate.DestroyIt();
        }

}
