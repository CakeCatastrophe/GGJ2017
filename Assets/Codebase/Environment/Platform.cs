using UnityEngine;
using System.Collections;

public class Platform : WaveTarget {
    

   public Vector3 m_minOffset = new Vector3(0,-1,0);
    public Vector3 m_maxOffset = new Vector3(0, 1, 0);
    float m_movePerSecond = 0.2f;
    Vector3 m_anchorPos;
    Vector3 m_startPos;
    Vector3 m_endPos;
    float m_currentChargeLevel = 0.0f;

    public bool m_can_reset = false;
    public float m_reset_time = 10f;
    GameTimer m_reset_timer;
    GameTimer m_reset_movement_timer;

    Vector3 m_last_set_position = Vector3.zero;

    // Use this for initialization
    void Start () {
        m_last_set_position = transform.position;
        m_reset_timer = new GameTimer(m_reset_time,false,null); 
        m_reset_movement_timer = new GameTimer(0.5f,false,null); 
        UpdateData();
        SetUpMats();
    }

    void Update() {
        if(!m_can_reset) {return;}
        m_reset_timer.Update();
        if(m_reset_timer.IsComplete()) {
            m_reset_movement_timer.Update();
            m_currentChargeLevel = 0.5f;
            transform.position = Vector3.Lerp(m_last_set_position,m_anchorPos,Mathf.Clamp(m_reset_movement_timer.GetProgress(),0,1));
        }
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
	public override void WaveUpdate (WaveGun wavegun) {
        if (wavegun.GetWaveSpeed()==WaveSpeed.SLOW)
        {
            SetChargeLevel(m_currentChargeLevel + m_movePerSecond * Time.deltaTime);
        }
        if (wavegun.GetWaveSpeed()==WaveSpeed.FAST)
        {
            SetChargeLevel(m_currentChargeLevel - m_movePerSecond * Time.deltaTime);
        }
        m_reset_timer.ResetTime();
        m_reset_movement_timer.ResetTime();

        base.WaveUpdate(wavegun);
    }

    void SetChargeLevel(float newChargeLevel)
    {
        newChargeLevel = Mathf.Clamp01(newChargeLevel);
        m_currentChargeLevel = newChargeLevel;
        Vector3 diff = m_maxOffset - m_minOffset;

        Vector3 newPos = m_startPos + newChargeLevel * diff;

        gameObject.transform.position = newPos;
        m_last_set_position = newPos;
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
