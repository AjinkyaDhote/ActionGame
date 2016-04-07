using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerWithEnemy : MonoBehaviour {

    Image HealthBar;
    Image EnergyBar;
    Text bulletText;
    public int bulletCount = 0;
    private BoxCollider col;
    float fillHealth = 1.0f;
    float fillEnergy = 1.0f;
    string bulletsString;
    
    // Use this for initialization
    void Start () {
        HealthBar = transform.FindChild("Main Camera").transform.FindChild("FPS UI Canvas").FindChild("healthBar").GetComponent<Image>();
        EnergyBar = transform.FindChild("Main Camera").transform.FindChild("FPS UI Canvas").FindChild("energyBar").GetComponent<Image>();
        bulletText = transform.FindChild("Main Camera").transform.FindChild("FPS UI Canvas").FindChild("bullets").GetComponent<Text>();
        bulletsString = "Bullets :" + bulletCount;
        bulletText.text = bulletsString;
        bulletText.color = Color.red;
    }
    int i = 0;

    void Awake()
    {

        col = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update () {
        i++;
        if (i % 10 == 0)
        {
            fillEnergy -= (float)(Time.deltaTime * 0.2);
            EnergyBar.fillAmount = fillEnergy;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (bulletCount > 0)
            {
                Debug.Log("Pressed left click.");
                bulletCount -= 1;
                bulletsString = "Bullets :" + bulletCount;
                bulletText.text = bulletsString;
                if (bulletCount == 0)
                {
                    bulletText.color = Color.red;
                }
            }
            else
            {
                bulletText.color = Color.red;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "enemy")
        {
            fillHealth -= 0.1f;
            HealthBar.fillAmount = fillHealth;

        }


        else if (other.name == "bat")
        {
            fillEnergy = 1f;
            EnergyBar.fillAmount = fillEnergy;

        }

        else if (other.name == "bullets")
        {
            bulletCount += 10;
            bulletsString = "Bullets :" + bulletCount;
            bulletText.text = bulletsString;
            bulletText.color = Color.black;


        }
      

    }
}
