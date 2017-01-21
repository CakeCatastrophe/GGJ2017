using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
    

   public Vector3 m_minOffset = new Vector3(0,-1,0);
    public Vector3 m_maxOffset = new Vector3(0, 1, 0);
    float m_movePerSecond = 1;
    Vector3 m_anchorPos;
    Vector3 m_startPos;
    Vector3 m_endPos;
    float m_currentChargeLevel = 0.0f;
    // Use this for initialization
    void Start () {
        UpdateData();
    }

    public Vector3 StartPos {
        get
        {
            if (Application.isPlaying == false)
            {
                UpdateData();
            }
            return m_startPos;
        }

        }

    public Vector3 EndPos
    {
        get
        {
            if (Application.isPlaying == false)
            {
                UpdateData();
            }
            return m_endPos;
        }

    }
    public Vector3 AnchorPos
    {
        get
        {
            if (Application.isPlaying == false)
            {
                UpdateData();
            }
            return m_anchorPos;
        }

    }
    void UpdateData()
    {
        m_anchorPos = gameObject.transform.position;
        m_startPos = m_anchorPos + m_minOffset;
        m_endPos = m_anchorPos + m_maxOffset;

        //fork out current offset
        float diff = (m_anchorPos - m_startPos).magnitude;
        float range = (m_minOffset - m_maxOffset).magnitude;
        if (range == 0)
        {
            Debug.LogError("why 0 range?");
        }
        else
        {
            m_currentChargeLevel = (diff / range);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey (KeyCode.Alpha1))
        {
            SetChargeLevel(m_currentChargeLevel + m_movePerSecond * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SetChargeLevel(m_currentChargeLevel - m_movePerSecond * Time.deltaTime);
        }
    }

    void SetChargeLevel(float newChargeLevel)
    {
        newChargeLevel = Mathf.Clamp01(newChargeLevel);
        m_currentChargeLevel = newChargeLevel;
        Vector3 diff = m_maxOffset - m_minOffset;

        Vector3 newPos = m_startPos + newChargeLevel * diff;

        gameObject.transform.position = newPos;
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
        {
            m_anchorPos = gameObject.transform.position;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_anchorPos + m_minOffset, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(m_anchorPos + m_maxOffset, 0.1f);
    }
}
