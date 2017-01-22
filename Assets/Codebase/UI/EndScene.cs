using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {
    public float m_timeToWait=1;
    public string m_sceneName = "main_scene";
    bool m_sceneLoadRequested = false;
    public float m_startTime;
    // Use this for initialization
    void Start () {
        m_startTime = Time.time;

    }
	
	// Update is called once per frame
	void Update () {
        if (m_sceneLoadRequested == false)
        {
            float timeSinceStart = Time.time- m_startTime;
            if (timeSinceStart > m_timeToWait)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(m_sceneName);
                m_sceneLoadRequested = true;
            }

        }

    }
}
