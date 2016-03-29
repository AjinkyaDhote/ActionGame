using UnityEngine;
using System.Collections;

public class RayCastShootScript : MonoBehaviour
{

    public float fireRate = .25f; //every quarter of a second the player can fire
    public float weaponRange;
    //public ParticleSystem shootParticles;
    public GameObject hitParticles;
    public int damage = 1;
    float duration = 100.0f;
    public GameObject GunCrosshair;
    public Animator anim;
    public AudioClip aclip;
     
    public GameObject gameManager;

    AudioSource source;
    public Camera myCam;
    private WaitForSeconds shotLength = new WaitForSeconds(.07f);
    private float nextFire;


    // Use this for initialization
    void Start()
    {

        anim = GetComponentInParent<Animator>();
        source = GetComponent<AudioSource>();
        aclip = source.clip;

    }

    // Update is called once per frame

    void Update()
    {
        if (!gameManager.GetComponent<GameStartScript>().scene2D)
        {
            RaycastHit hit;
            Vector3 rayOrigin = myCam.ViewportToWorldPoint(GunCrosshair.transform.position);
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
            {
                anim.SetTrigger("Shooting");
                source.PlayOneShot(aclip, 1);
                Debug.Log("Fire Button Clicked");
                nextFire = Time.time + fireRate; //check what is fireRate

                if (Physics.Raycast(transform.position, transform.forward, out hit, weaponRange))
                {


                    Debug.Log(hit.collider.gameObject.name);

                    Debug.DrawRay(transform.position, Vector3.forward, Color.red, duration);
                    Debug.DrawRay(transform.position, transform.forward, Color.red, duration, true);
                    Debug.Log("Ray Created");
                    if (hit.collider.GetType() == typeof(CapsuleCollider))
                    {
                        EnemyHealth dmgScript = hit.collider.GetComponent<EnemyHealth>();
                        Debug.Log("Enemy Hit");
                        if (dmgScript != null)
                        {

                            // Check what is Quaternion.;identity
                            dmgScript.Damage(damage, hit.point);
                        }
                    }
                }
            }
        }
    }
}
