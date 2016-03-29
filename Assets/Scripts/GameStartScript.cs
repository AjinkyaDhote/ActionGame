using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStartScript : MonoBehaviour
{
	public Camera camera2DScene = null;
	public Camera camera3DScene = null;
    public GameObject player3D = null;
    public GameObject enemy1 = null;
    public GameObject enemy2 = null;
    public GameObject enemy3 = null;
    public GameObject enemy4 = null;

    public bool scene2D = true;

	void Awake ()
	{
        SwitchTO2DScene();
    }
	


    void SwitchTO2DScene()
    {
        if (camera2DScene!= null)
        {
            camera2DScene.enabled = true;
            AudioListener camera2DSceneAL = camera2DScene.GetComponent<AudioListener>();
            camera2DSceneAL.enabled = true;

            camera3DScene.enabled = false;
            AudioListener camera3DSceneAL = camera3DScene.GetComponent<AudioListener>();
            camera3DSceneAL.enabled = false;
        }
    }

    void SwitchTO3DScene()
    {
        if (camera2DScene != null)
        {
            camera2DScene.enabled = false;
            AudioListener camera2DSceneAL = camera2DScene.GetComponent<AudioListener>();
            camera2DSceneAL.enabled = false;

            camera3DScene.enabled = true;
            AudioListener camera3DSceneAL = camera3DScene.GetComponent<AudioListener>();
            camera3DSceneAL.enabled = true;

            player3D.GetComponent<Player3DScript>().InitializePlayer();
            enemy1.GetComponent<EnemyAI>().InitializeEnemy();
            enemy2.GetComponent<EnemyAI>().InitializeEnemy();
            enemy3.GetComponent<EnemyAI>().InitializeEnemy();
            enemy4.GetComponent<EnemyAI>().InitializeEnemy();
        }
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitGame()
    {
        Application.Quit();

    }

    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            scene2D = !scene2D;
            if (scene2D)
            {
                SwitchTO2DScene();
            }
            else
			{
                SwitchTO3DScene();
            }
        }
    }
}