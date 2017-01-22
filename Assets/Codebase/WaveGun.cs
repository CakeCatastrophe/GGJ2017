using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveGun : MonoBehaviour {

    static WaveGun s_instance = null;

    static public WaveGun Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = GameObject.FindObjectOfType<WaveGun>();
            }
            return s_instance;
        }
    }


    List<Objective> m_completedObjectives = new List<Objective>();
	ParticleSystem m_particle_system;
	ParticleSystem m_particle_subsystem;
	UnityStandardAssets.Characters.FirstPerson.FirstPersonController m_fps_controller;

	Vector3 m_wave_min_pos = new Vector3(-0.2f,-0.4f,0);
	Vector3 m_wave_max_pos = new Vector3(0.2f,-0.4f,0);

	int m_wave_direction = 1;
	float m_direction_timer = 0;
	List<float> m_previous_dir_times = new List<float>();
	float m_previous_wave_position = 0;
	Vector3 m_previous_mouse_position = Vector3.zero;

	GameTimer m_wave_stationary_timer;

	WaveTarget m_wave_target;

	AudioSource m_audio;
	public AudioClip[] m_note_sounds;

	public bool m_has_target = false;
	float m_max_range = 8;

	CharacterController m_char_controller;


	void Start () {
        s_instance = this;
        m_wave_stationary_timer = new GameTimer(0.5f,false,ResetWave);
		m_audio = GetComponent<AudioSource>();
		m_particle_system = GetComponent<ParticleSystem>();
		//m_particle_subsystem = transform.Find("Spark").GetComponent<ParticleSystem>();
		m_fps_controller = transform.parent.parent.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
		
		m_char_controller = m_fps_controller.GetComponent<CharacterController>();
	}
	
	void Update () {
		if(!m_char_controller.isGrounded) {
			return;
		}

		RaycastHit underfoot;
		Physics.Raycast(transform.position,Vector3.down,out underfoot,1);

		if(Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			if(Physics.Raycast(transform.position,transform.forward,out hit,m_max_range)){
				m_wave_target = hit.transform.gameObject.GetComponent<WaveTarget>();

				if(m_wave_target!=null && underfoot.transform!=null  && m_wave_target.gameObject==underfoot.transform.gameObject) {
					m_wave_target = null;
				}
			}
			else {
				m_wave_target = null; 
			}
			if(m_wave_target!=null) {
				m_wave_target.WaveStart(this);
			}
			
			ResetWave();
		}

		if(Input.GetMouseButtonUp(0)) {
			if(m_wave_target!=null) {
				m_wave_target.WaveEnd(this);
			}
			m_wave_target=null;
			ResetWave();
		}

		if(m_wave_target!=null) {
			m_wave_target.WaveUpdate(this);
		}

		if(m_wave_target!=null) {
			transform.parent.LookAt(m_wave_target.transform);
		}

		bool is_waving = Input.GetMouseButton(0);
		transform.localPosition = Vector3.Lerp(m_wave_min_pos,m_wave_max_pos,Mathf.Clamp(Input.mousePosition.x/Screen.width,0,1));

		m_particle_system.enableEmission = is_waving;
		m_fps_controller.enabled = !is_waving;


		UpdateWaveDirection(is_waving);
		UpdateWaveColour();
		UpdateSound();
		UpdateCursorState(is_waving);
		UpdateTarget();
	}

	void UpdateWaveDirection(bool is_waving) {
		if(!is_waving) {return;}

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

	void UpdateSound() {
		if(m_note_sounds.Length<3) {
			return;
		}

		m_audio.mute = false;

		switch(GetWaveSpeed()) {
			case WaveSpeed.FAST:
				if(m_audio.clip!=m_note_sounds[0]) {
					m_audio.clip = m_note_sounds[0];
					m_audio.Play();
				}
				return;
			case WaveSpeed.MEDIUM:
				if(m_audio.clip!=m_note_sounds[1]) {
					m_audio.clip = m_note_sounds[1];
					m_audio.Play();
				}
				return;
			case WaveSpeed.SLOW:
				if(m_audio.clip!=m_note_sounds[2]) {
					m_audio.clip = m_note_sounds[2];
					m_audio.Play();
				}
				return;
			default:
				m_audio.mute = true;
				return;
		}
	}

	public float GetAverageWaveTime() {
		if(m_previous_dir_times.Count==0) {return float.PositiveInfinity;}

		float t = 0;
		foreach(float f in m_previous_dir_times) {
			t += f;
		}
		t = t/m_previous_dir_times.Count;
		return t;
	}

	public WaveSpeed GetWaveSpeed() {
		float m_avg_time = GetAverageWaveTime();
		if(m_avg_time<0.14f) {
			return WaveSpeed.FAST;
		}
		else if(m_avg_time<0.4f) {
			return WaveSpeed.MEDIUM;
		}
		else if(m_avg_time<float.PositiveInfinity) {
			return WaveSpeed.SLOW;
		}
		return WaveSpeed.STATIC;
	}

	public Color GetWaveColor() {
		return m_particle_system.startColor;
	}

	public int GetWaveDirection() {
		return m_wave_direction;
	}

	void UpdateWaveColour() {
		switch(GetWaveSpeed()) {
			case WaveSpeed.FAST:
				m_particle_system.startColor = new Color(0.95f,0.02f,0.02f); //Color.red;
				return;
			case WaveSpeed.MEDIUM:
				m_particle_system.startColor = Color.yellow;
				return;
			case WaveSpeed.SLOW:
				m_particle_system.startColor = new Color(0.02f,0.02f,0.95f);//Color.blue;
				return;
			case WaveSpeed.STATIC:
				m_particle_system.startColor = Color.white;
				return;
		}
	}

	void UpdateTarget() {
		RaycastHit hit;
		if(Physics.Raycast(transform.position,transform.forward,out hit,m_max_range)){
			m_has_target = hit.transform.gameObject.GetComponent<WaveTarget>()!=null;
		}
		else {
			m_has_target = false;
		}
	}

	void ResetWave() {
		m_previous_mouse_position = Input.mousePosition;
		m_previous_dir_times = new List<float>();
		m_previous_wave_position = 0;
	}

    internal bool IsObjectiveComplete(int objectiveID)
    {
        for (int i = 0; i < m_completedObjectives.Count; i++)
        {
            Objective currentObjective = m_completedObjectives[i];
            if (currentObjective.m_objectiveID == objectiveID)
            {
                return true;
            }
        }
        return false;
    }
    internal void CompleteObjective(Objective currentObjective)
    {
        if (m_completedObjectives.Contains(currentObjective) == false)
        {
            Debug.Log("Objective Complete " + currentObjective.m_objectiveID);
            m_completedObjectives.Add(currentObjective);
        }
    }


}

public enum WaveSpeed {
	STATIC,
	SLOW,
	MEDIUM,
	FAST
}