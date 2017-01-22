using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    float m_splashTime = 1;
    float m_startTime = 1;
    bool m_open = true;
    public GameObject m_startScreen = null;

    // Use this for initialization
    void Start () {
        m_startTime = Time.time;
        UIController.Instance.SetGameElements(false);
    }
	
	// Update is called once per frame
	void Update () {
        float timeSinceStart = Time.time - m_startTime;
        if (m_open)
        {
            if (timeSinceStart > m_splashTime)
            {
                m_startScreen.SetActive(true);
                
                m_open = false;
                Destroy(gameObject);
            }
        }

    }
}
