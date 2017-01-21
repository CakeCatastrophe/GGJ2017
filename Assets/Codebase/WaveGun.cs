using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveGun : MonoBehaviour {
	ParticleSystem m_particle_system;
	UnityStandardAssets.Characters.FirstPerson.FirstPersonController m_fps_controller;

	Vector3 m_wave_min_pos = new Vector3(-0.5f,-0.4f,0);
	Vector3 m_wave_max_pos = new Vector3(0.5f,-0.4f,0);

	int m_wave_direction = 1;
	float m_direction_timer = 0;
	List<float> m_previous_dir_times = new List<float>();
	float m_previous_wave_position = 0;



	void Start () {
		m_particle_system = GetComponent<ParticleSystem>();
		m_fps_controller = transform.parent.parent.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			m_previous_dir_times = new List<float>();
			m_previous_wave_position = 0;
		}

		bool is_waving = Input.GetMouseButton(0);
		transform.localPosition = Vector3.Lerp(m_wave_min_pos,m_wave_max_pos,Mathf.Clamp(Input.mousePosition.x/Screen.width,0,1));

		m_particle_system.enableEmission = is_waving;
		m_fps_controller.enabled = !is_waving;


		UpdateWaveDirection();
		UpdateWaveColour();
		UpdateCursorState(is_waving);
	}

	void UpdateWaveDirection() {
		m_direction_timer+=Time.deltaTime;

		if(transform.localPosition.x<m_previous_wave_position && m_wave_direction==1) {
			m_wave_direction = -1;
			m_previous_dir_times.Add(m_direction_timer);
			if(m_previous_dir_times.Count>4) {
				m_previous_dir_times.RemoveAt(0);
			}
			m_direction_timer = 0;
		}
		else if(transform.localPosition.x>m_previous_wave_position && m_wave_direction==-1) {
			m_wave_direction = 1;
			m_previous_dir_times.Add(m_direction_timer);
			if(m_previous_dir_times.Count>4) {
				m_previous_dir_times.RemoveAt(0);
			}
			m_direction_timer = 0;
		}

		m_previous_wave_position = transform.localPosition.x;
	}

	void UpdateCursorState(bool is_waving) {
		Cursor.visible = false;
		if(is_waving) {
			Cursor.lockState = CursorLockMode.None;
		}
		else {
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	float GetAverageWaveTime() {
		if(m_previous_dir_times.Count==0) {return 10;}

		float t = 0;
		foreach(float f in m_previous_dir_times) {
			t += f;
		}
		t = t/m_previous_dir_times.Count;
		return t;
	}

	void UpdateWaveColour() {
		float m_avg_time = GetAverageWaveTime();
		if(m_avg_time<0.14f) {
			m_particle_system.startColor = Color.red;
		}
		else if(m_avg_time<0.4f) {
			m_particle_system.startColor = Color.yellow;
		}
		else {
			m_particle_system.startColor = Color.blue;
		}
	}
}
