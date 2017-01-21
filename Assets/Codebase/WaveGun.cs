using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveGun : MonoBehaviour {
	ParticleSystem m_particle_system;
	ParticleSystem m_particle_subsystem;
	UnityStandardAssets.Characters.FirstPerson.FirstPersonController m_fps_controller;

	Vector3 m_wave_min_pos = new Vector3(-0.5f,-0.4f,0);
	Vector3 m_wave_max_pos = new Vector3(0.5f,-0.4f,0);

	int m_wave_direction = 1;
	float m_direction_timer = 0;
	List<float> m_previous_dir_times = new List<float>();
	float m_previous_wave_position = 0;
	Vector3 m_previous_mouse_position = Vector3.zero;

	GameTimer m_wave_stationary_timer;

	WaveTarget m_wave_target;


	void Start () {
		m_wave_stationary_timer = new GameTimer(0.5f,false,ResetWave);
		m_particle_system = GetComponent<ParticleSystem>();
		//m_particle_subsystem = transform.Find("Spark").GetComponent<ParticleSystem>();
		m_fps_controller = transform.parent.parent.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			if(Physics.Raycast(transform.position,transform.forward,out hit)){
				m_wave_target = hit.transform.gameObject.GetComponent<WaveTarget>();
			}
			else {
				m_wave_target = null;
			}
			
			ResetWave();
		}

		if(m_wave_target!=null) {
			transform.parent.LookAt(m_wave_target.transform);
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

		if(m_previous_mouse_position==Input.mousePosition) {
			m_wave_stationary_timer.Update();
		}
		else {
			m_wave_stationary_timer.ResetTime();
		}

		m_previous_wave_position = transform.localPosition.x;
		m_previous_mouse_position = Input.mousePosition;
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
		if(m_previous_dir_times.Count==0) {return float.PositiveInfinity;}

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
			//m_particle_subsystem.startColor = Color.red;
		}
		else if(m_avg_time<0.4f) {
			m_particle_system.startColor = Color.yellow;
			//m_particle_subsystem.startColor = Color.yellow;
		}
		else if(m_avg_time<float.PositiveInfinity) {
			m_particle_system.startColor = Color.blue;
			//m_particle_subsystem.startColor = Color.blue;
		}
		else {
			m_particle_system.startColor = Color.white;
			//m_particle_subsystem.startColor = Color.white;
		}
	}

	void ResetWave() {
		m_previous_mouse_position = Input.mousePosition;
		m_previous_dir_times = new List<float>();
		m_previous_wave_position = 0;
	}
}
