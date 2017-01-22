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
    // Use this for initialization
    void Start () {
        s_instance = this;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
