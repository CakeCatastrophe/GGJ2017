using UnityEngine;
using System.Collections;

public class WaveTarget : MonoBehaviour {

    Color m_currentCol = Color.white;
    public Material[] m_mats = null;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public virtual void WaveStart(WaveGun wavegun) {

	}

	public virtual void WaveUpdate(WaveGun wavegun) {
        UpdateMatCols(wavegun.GetWaveColor());

    }

	public virtual void WaveEnd(WaveGun wavegun) {

	}
    void UpdateMatCols(Color newCol)
    {

        if (m_mats != null)
        {
            if (m_currentCol == newCol)
            {
                return;
            }

            m_currentCol = newCol;
            for (int i = 0; i < m_mats.Length; i++)
            {
                Material currentMat = m_mats[i];
                if (currentMat != null)
                {
                    currentMat.SetColor("_Emission", newCol);
                }
            }
        }
    }
}
