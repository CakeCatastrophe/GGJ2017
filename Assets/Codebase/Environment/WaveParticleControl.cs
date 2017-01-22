using UnityEngine;
using System.Collections;

public class WaveParticleControl : MonoBehaviour {
    float m_currentSpeed = 0;
    public float m_maxOffset = 1;
    public float m_startSpeed = 1;
	// Use this for initialization
	void Start () {
        m_currentSpeed = m_startSpeed;

    }
    public float m_anglePerSec = 360;
    float m_angle = 0;
    // Update is called once per frame
    void Update () {

        Vector3 newPos = transform.localPosition;

        if (m_currentSpeed > 0 && newPos.x > m_maxOffset)
        {
            m_currentSpeed = -m_currentSpeed;
        }
        else if(m_currentSpeed < 0 && newPos.x < -m_maxOffset)
        {
            m_currentSpeed = -m_currentSpeed;
        }

        float ratio = 1;
        ratio = 1 - Mathf.Abs(newPos.x) / m_maxOffset;
        if (ratio < 0.01f)
        {
            ratio = 0.01f;
        }
       // newPos = newPos + new Vector3(ratio, 0, 0) * m_currentSpeed * Time.deltaTime;

        m_angle += m_anglePerSec * Time.deltaTime;

        if (m_angle > 360)
        {
            m_angle = m_angle - 360;
        }
        else if (m_angle < -360)
        {
            m_angle = m_angle + 360;
        }
       
        ratio = Mathf.Cos(Mathf.Deg2Rad*m_angle);
        newPos =  new Vector3(ratio, 0, 0) * m_maxOffset;
        transform.localPosition = newPos;
    }
}
