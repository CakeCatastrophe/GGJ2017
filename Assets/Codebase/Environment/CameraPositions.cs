using UnityEngine;
using System.Collections;

public class CameraPositions : MonoBehaviour {

    static CameraPositions s_instance = null;
    static public CameraPositions Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = GameObject.FindObjectOfType<CameraPositions>();
            }
            return s_instance;
        }
    }

    public GameObject m_camera = null;
    public GameObject m_opera = null;
    public Animator m_operaAnimator = null;
    public SkinnedMeshRenderer m_operaRender = null;


    public GameObject m_camMenu = null;
    public GameObject m_camend1 = null;
    public GameObject m_camend2 = null;


    public GameObject m_operamain = null;
    public GameObject m_operaend1 = null;
    public GameObject m_operaend2 = null;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    internal void MoveObjectToPosition(GameObject objectToMove, GameObject newRoot)
    {
        if (objectToMove != null && newRoot != null)
        {
            objectToMove.transform.SetParent(newRoot.transform);
            objectToMove.transform.localPosition = Vector3.zero;
            objectToMove.transform.localEulerAngles = Vector3.zero;
        }

    }
    internal void SetAnim(string name)
    {
        m_operaAnimator.Play(name);
    }

    internal void TurnLightOn()
    {
       Material[] allMats =   m_operaRender.sharedMaterials;

        for (int i = 0; i < allMats.Length; i++)
        {
          
            allMats[i].SetColor("_EmissionColor", Color.white);
            
        }
        m_operaRender.sharedMaterials = allMats;
        


    }

}
