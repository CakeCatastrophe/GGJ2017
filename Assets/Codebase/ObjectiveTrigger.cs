using UnityEngine;
using System.Collections.Generic;
using System;


public class ObjectiveTrigger: MonoBehaviour {

    bool m_hasTriggered = false;
    public bool m_useCollision = false;

    public List<WaveCrystal> m_crystalLinks = null;

    public List<Objective> m_requiredObjectives = new List<Objective>();

    public List<Objective> m_completesObjectives = new List<Objective>();

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (m_hasTriggered == false)
        {
            if (m_crystalLinks != null&& m_crystalLinks.Count>0)
            {
                if (CheckCrystals()==true && AreRequiredObjectivesComplete(WaveGun.Instance))
                {
                    PlayCompleteActions(WaveGun.Instance);
                }
            }
        }
    }
    bool CheckCrystals()
    {
        for (int i = 0; i < m_crystalLinks.Count; i++)
        {
            WaveCrystal crystalLink = m_crystalLinks[i];
            if (crystalLink.GetIsUnlocked()==false)
            {
                return false;
            }
        }
     
        return true;
    }

    internal virtual void PlayCompleteActions(WaveGun player)
    {
        CompleteObjectives(player);


    }
    bool AreRequiredObjectivesComplete(WaveGun player)
    {
        for (int i = 0; i < m_requiredObjectives.Count; i++)
        {
            Objective currentObjective = m_requiredObjectives[i];
            if (player.IsObjectiveComplete(currentObjective.m_objectiveID) == false)
            {
                return false;
            }
        }
        return true;
    }
    void CompleteObjectives(WaveGun player)
    {
        for (int i = 0; i < m_completesObjectives.Count; i++)
        {
            Objective currentObjective = m_completesObjectives[i];
            player.CompleteObjective(currentObjective);

        }
    }
    void OnTriggerEnter(Collider other)
    {
        ActOnCollision(other);
    }

    void ActOnCollision(Collider other)
    {
        if (m_useCollision == true && other.gameObject == WaveGun.Instance.gameObject)
        {
            if (AreRequiredObjectivesComplete(WaveGun.Instance))
            {
                PlayCompleteActions(WaveGun.Instance);
            }
        }
    }
}

