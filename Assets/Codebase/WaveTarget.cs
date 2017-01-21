using UnityEngine;
using System.Collections.Generic;

public class WaveTarget : MonoBehaviour {

    Color m_currentCol = Color.white;
    List<Material> m_mats = new List<Material>();

	void Start () {
        SetUpMats();
    }
    protected void SetUpMats()
    {
        MeshRenderer[] allMeshes = gameObject.GetComponentsInChildren<MeshRenderer>(true);
        for (int i = 0; i < allMeshes.Length; i++)
        {


            m_mats.AddRange(allMeshes[i].materials);
        }
        allMeshes = gameObject.GetComponents<MeshRenderer>();
        for (int i = 0; i < allMeshes.Length; i++)
        {


            m_mats.AddRange(allMeshes[i].materials);
        }
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
            for (int i = 0; i < m_mats.Count; i++)
            {
                Material currentMat = m_mats[i];
                if (currentMat != null)
                {
                    currentMat.SetColor("_EmissionColor", newCol);
                }
            }
        }
    }
}
