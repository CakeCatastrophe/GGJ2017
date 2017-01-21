﻿using UnityEngine;
using System.Collections;

public class CrystalDoor : MonoBehaviour {
	public GameObject[] m_cystals;
	Vector3 m_base_position;
	public Vector3 m_new_position = Vector3.up;

	GameTimer m_open_time = new GameTimer(1,false,null);

	// Use this for initialization
	void Start () {
		m_base_position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(IsUnlocked()) {
			m_open_time.Update();
			transform.position = Vector3.Lerp(m_base_position,m_base_position+m_new_position,Mathf.Clamp(m_open_time.GetProgress(),0,1));
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
}