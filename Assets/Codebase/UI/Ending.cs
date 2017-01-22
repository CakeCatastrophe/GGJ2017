using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ending : MonoBehaviour {

    enum EndingStates
    {
        None,
        StartEnding,
        Story1,
        Story2,
        TheEnd,
        Close,
        LoadEndScreen
    }
    public string m_sceneName = "EndScene";
    EndingStates m_currentState = EndingStates.None;
    EndingStates m_nextState = EndingStates.None;
    public Text m_lblEndInfo = null;
    public CanvasGroup m_blackBack = null;
    public float m_timeForStates = 2;

    float m_stateStart = 0;
    // Use this for initialization
    void Start () {
        m_nextState = EndingStates.StartEnding;

    }
	
	// Update is called once per frame
	void Update () {

        if (m_nextState != EndingStates.None && m_nextState != m_currentState)
        {
            StartState(m_nextState);
        }
        UpdateState(m_currentState);
	}

    void StartState(EndingStates newState)
    {
        switch (newState)
        {
            case EndingStates.StartEnding:
            UIController.Instance.SetGameElements(false);
            break;
            case EndingStates.Story1:
            m_lblEndInfo.color = new Color(m_lblEndInfo.color.r, m_lblEndInfo.color.g, m_lblEndInfo.color.b, 1);
            m_lblEndInfo.SetAllDirty();
            m_lblEndInfo.text = "And so ...";
            break;
            case EndingStates.Story2:
            m_lblEndInfo.SetAllDirty();
            m_lblEndInfo.text = "It Ended";
            break;
            case EndingStates.TheEnd:
            m_blackBack.alpha = 1;
            m_lblEndInfo.SetAllDirty();
            m_lblEndInfo.text = "The End!";
            break;
            case EndingStates.Close:
            m_lblEndInfo.SetAllDirty();
            m_lblEndInfo.CrossFadeAlpha(0, 1, false);
            break;
            case EndingStates.LoadEndScreen:
            UnityEngine.SceneManagement.SceneManager.LoadScene(m_sceneName);

            break;
     
        }
        m_stateStart = Time.time;
        m_currentState = newState;
    }
    void SetNextState(EndingStates newState)
    {
        m_nextState = newState;
    }

    void UpdateState(EndingStates newState)
    {
        float timeInState = Time.time- m_stateStart  ;
        switch (newState)
        {
            case EndingStates.StartEnding:          
            case EndingStates.Story1:
            case EndingStates.Story2:
            case EndingStates.TheEnd:
            case EndingStates.Close:
            {
                if (timeInState > m_timeForStates)
                {
                    SetNextState(newState + 1);
                }
                break;
            }
            case EndingStates.LoadEndScreen:
            break;

        }

    }
}
