using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour {

	public GameObject muzzleFlash; //Gun Muzzle Flash
	ParticleSystem muzzleFlashPS;
	public Animator anim;
	public PlayerWithEnemy pScript;
	public GameObject impactPrefab; //Bullet Particle Impact

	GameObject muzzleFlashInstance;
	bool shooting = false;
    public int bulletCount = 0;
	GameObject[] impacts;
	int currentImpact = 0;
	int maxImpacts = 5;
     

	// Use this for initialization
	void Start () {
		 muzzleFlashInstance = (GameObject) Instantiate (muzzleFlash, new Vector3 (transform.position.x + 0.607f, transform.position.y - 0.59f , transform.position.z + 2.017f), Quaternion.identity);
		//muzzleFlash.transform.position = new Vector3 (transform.position.x + 0.607f, transform.position.y - 0.59f, transform.position.z + 2.017f);
		//muzzleFlash.transform.localRotation = Quaternion.identity;
		muzzleFlashPS = muzzleFlashInstance.GetComponent<ParticleSystem> ();
		impacts = new GameObject[maxImpacts];
		for (int i = 0; i < maxImpacts; i++) 
		{
			impacts [i] = Instantiate (impactPrefab);
		}
        //pScript = transform.parent.parent.GetComponent<PlayerWithEnemy>();
		anim = GetComponentInChildren<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")) 
		{
			muzzleFlashInstance.transform.position = new Vector3 (transform.position.x + 0.607f, transform.position.y - 0.59f, transform.position.z + 2.017f);
			muzzleFlash.transform.localRotation = transform.rotation;
			//GameObject muzzleFlashInstance = (GameObject) Instantiate (muzzleFlash, new Vector3 (transform.position.x + 0.607f, transform.position.y - 0.59f , transform.position.z + 2.017f), Quaternion.identity);
			//muzzleFlashInstance.GetComponent<ParticleSystem> ().Play();
			muzzleFlashPS.Play();
			anim.SetTrigger ("Fire");
			shooting = true;
		}
        bulletCount = pScript.bulletCount;
			
	}


	void FixedUpdate()
	{
		if (shooting && bulletCount >0) 
		{
			shooting = false;
			RaycastHit hit;

			if(Physics.Raycast(transform.position,transform.forward,out hit, 50f))
			{
				impacts [currentImpact].transform.position = hit.point;
				impacts [currentImpact].GetComponent<ParticleSystem> ().Play ();
				if (++currentImpact >= maxImpacts)
					currentImpact = 0;
			}
		
		}
	}
}
