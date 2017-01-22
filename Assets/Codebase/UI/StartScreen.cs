using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartGame();
        }
	}
    void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main_scene");
        Destroy(gameObject);
    }
}
