using UnityEngine;
using System.Collections;

public class ObjectiveEndTrigger : ObjectiveTrigger
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CrystalUpdate();
    }
    internal override void PlayCompleteActions(WaveGun player)
    {
        base.PlayCompleteActions(player);
        if (UIController.Instance != null && UIController.Instance.m_endPage != null)
        {
            UIController.Instance.m_endPage.gameObject.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        ActOnCollision(other);
    }
}
