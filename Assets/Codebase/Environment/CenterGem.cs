using UnityEngine;
using System.Collections.Generic;

public class CenterGem : MonoBehaviour {

    static CenterGem s_instance = null;
    static public CenterGem Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = GameObject.FindObjectOfType<CenterGem>();
            }
            return s_instance;
        }
    }
    public GameObject m_objective1 = null;
    public GameObject m_objective2 = null;
    public GameObject m_objective3 = null;
    public GameObject m_objective10 = null;

    public MeshRenderer m_mesh1 = null;
    public MeshRenderer m_mesh2 = null;
    public MeshRenderer m_mesh3 = null;
    public MeshRenderer m_mesh10 = null;


    public Color m_col1 = Color.white;
    public Color m_col2 = Color.white;
    public Color m_col3 = Color.white;
    public Color m_col10 = Color.white;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void ObjectiveCompleted(int objectiveID)

    {
        switch (objectiveID)
        {
            case 1:
            m_objective1.SetActive(true);
            UpdateMats(m_mesh1,m_col1);
            break;
            case 2:
            m_objective2.SetActive(true);
            UpdateMats(m_mesh2, m_col2);
            break;
            case 3:
            m_objective3.SetActive(true);
            UpdateMats(m_mesh3, m_col3);
            break;
            case 10:
            m_objective10.SetActive(true);
            UpdateMats(m_mesh10, m_col10);
            break;
        }
    }
    void UpdateMats(MeshRenderer currentMesh, Color newCol)
    {
        Material[] mats = currentMesh.materials;
        for (int i = 0; i < mats.Length; i++)
        {
            Material currentMat = mats[i];
            if (currentMat != null)
            {
                currentMat.SetColor("_EmissionColor", newCol);
            }
        }
    }


}
