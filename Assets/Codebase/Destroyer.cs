using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
	public float m_time;

	GameTimer m_timer;

	// Use this for initialization
	void Start () {
		m_timer = new GameTimer(m_time,false,DieNow);
	}
	
	// Update is called once per frame
	void Update () {
		m_timer.Update();
	}

	void DieNow() {
		Destroy(gameObject);
	}
}
