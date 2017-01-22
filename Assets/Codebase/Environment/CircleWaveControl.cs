using UnityEngine;
using System.Collections;

public class CircleWaveControl : MonoBehaviour {
    public float m_anglePerSec = 90;
    float m_angle = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        m_angle += m_anglePerSec * Time.deltaTime;

        if (m_angle > 360)
        {
            m_angle = m_angle - 360;
        }
        else if (m_angle < -360)
        {
            m_angle = m_angle + 360;
        }

        transform.localEulerAngles =new Vector3(0, m_angle,0);
    }
}
