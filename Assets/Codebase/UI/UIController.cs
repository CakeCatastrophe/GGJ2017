using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    static UIController s_instance = null;
    static public UIController Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = GameObject.FindObjectOfType<UIController>();
            }
            return s_instance;
        }
    }

    public Ending m_endPage = null;
    public GameObject m_playerObject = null;
    public GameObject m_crossHairObject = null;
    public GameObject m_menuCam = null;
    // Use this for initialization
    void Start () {
        s_instance = this;

    }

    internal void SetGameElements(bool gameObjOn)
    {
        m_playerObject.SetActive(gameObjOn);
        m_crossHairObject.SetActive(gameObjOn);
        m_menuCam.SetActive(!gameObjOn);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
