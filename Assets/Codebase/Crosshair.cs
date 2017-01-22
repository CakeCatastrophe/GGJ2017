using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Crosshair : MonoBehaviour {
	Image m_image;

	// Use this for initialization
	void Start () {
		m_image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(WaveGun.Instance.m_has_target) {
			m_image.color = new Color(1,1,1,0.8f);
		}
		else {
			m_image.color = new Color(1,1,1,0.2f);
		}
	}
}
