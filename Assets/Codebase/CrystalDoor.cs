using UnityEngine;
using System.Collections;

public class CrystalDoor : MonoBehaviour {
	public GameObject[] m_cystals;
	Vector3 m_base_position;
	public Vector3 m_new_position = Vector3.up;

	public float m_door_time = 1;
	GameTimer m_open_time = new GameTimer(1,false,null);
	GameTimer m_close_time = new GameTimer(1,false,null);

	public bool m_resets;
	public float m_reset_time;
	GameTimer m_reset_timer = new GameTimer(1,false,null);

	bool m_open = false;

	// Use this for initialization
	void Start () {
		m_open_time = new GameTimer(m_door_time,false,null);
		m_close_time = new GameTimer(m_door_time,false,null);
		m_reset_timer = new GameTimer(m_reset_time,false,OnReset);
		m_base_position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(IsUnlocked() && (!m_resets || !m_reset_timer.IsComplete())) {
			m_open_time.Update();
			m_open = m_open_time.IsComplete();
			transform.position = Vector3.Lerp(m_base_position,m_base_position+m_new_position,Mathf.Clamp(m_open_time.GetProgress(),0,1));
			m_close_time.ResetTime();

			m_reset_timer.Update();
		}
		else {
			if(m_open || (m_resets && m_reset_timer.IsComplete())) {
				m_open_time.ResetTime();
				m_close_time.Update();
				transform.position = Vector3.Lerp(m_base_position+m_new_position,m_base_position,Mathf.Clamp(m_close_time.GetProgress(),0,1));
				
				m_reset_timer.ResetTime();
			}
		}
	}

	bool IsUnlocked() {
		bool unlocked = true;
		foreach(GameObject o in m_cystals) {
			if(o.GetComponent<WaveCrystal>()==null) {continue;}
			if(!o.GetComponent<WaveCrystal>().GetIsUnlocked()) {
				unlocked = false;
			}
		}
		return unlocked;
	}

	void OnReset() {
		foreach(GameObject o in m_cystals) {
			o.GetComponent<WaveCrystal>().m_unlocked = false;
			o.GetComponent<WaveCrystal>().SetColor(Color.white);
		}
	}


}
