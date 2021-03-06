﻿using UnityEngine;
using System.Collections;

public class WaveCrystal : WaveTarget {
	public WaveSpeed m_required_speed;
	Renderer m_renderer;

	public bool m_unlocked = false;
	WaveSpeed m_previous_speed = WaveSpeed.STATIC;

	GameTimer m_color_timer;

	public AudioClip m_audio_thing;

	bool m_played_sound = false;

	void Start() {
		m_renderer = GetComponent<Renderer>();
		m_color_timer = new GameTimer(1,false,null);
        SetUpMats();

    }

	public override void WaveStart(WaveGun wavegun) {

	}
	public override void WaveUpdate(WaveGun wavegun) {
		if(wavegun.GetWaveSpeed()==m_previous_speed) {
			m_color_timer.Update();
		}
		else {
			m_color_timer.ResetTime();
			m_played_sound = false;
		}

		if(m_color_timer.IsComplete()) {
			//m_renderer.material.color = wavegun.GetWaveColor();
			m_unlocked = wavegun.GetWaveSpeed()==m_required_speed;
            base.WaveUpdate(wavegun);

			if(m_played_sound == false && m_unlocked) {
				m_played_sound = true;
				GetComponent<AudioSource>().PlayOneShot(m_audio_thing);
			}
        }

		m_previous_speed = wavegun.GetWaveSpeed();
       
	}
	public override void WaveEnd(WaveGun wavegun) {

	}
	public bool GetIsUnlocked() {
		return m_unlocked;
	}

	public void SetColor(Color c) {
		base.UpdateMatCols(c);
	}
}
