using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 3;

	private int currentHealth;
	public GameObject hitParticles;
	


	// Use this for initialization
	void Start () {

		currentHealth = startingHealth;
		
	}

	public void Damage(int damage, Vector3 hitPoint)
	{
		Instantiate(hitParticles,hitPoint, Quaternion.identity);

		currentHealth -= damage;

		if (currentHealth <= 0) 
		{

			Defeated ();
		}

	}

	void Defeated()
	{
		//Play animation dying animation;
		gameObject.SetActive (false);

	}
}
