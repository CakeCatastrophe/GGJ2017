using UnityEngine;
using System.Collections.Generic;
using System;


public class ObjectiveTrigger: MonoBehaviour {

    bool m_hasTriggered = false;
    public bool m_useCollision = false;

    public List<WaveCrystal> m_crystalLinks = null;

    public List<Objective> m_requiredObjectives = new List<Objective>();

    public List<Objective> m_completesObjectives = new List<Objective>();

    public GameObject m_death_particle;

    public GameObject m_particle_isnt;

    GameTimer m_awsome_timer = new GameTimer(3,false,null);

    public AudioClip m_waah;

    bool m_has_played_audio = false;

    bool m_blah = false;



    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        CrystalUpdate();
         
    }
    protected void CrystalUpdate()
    {
        if (m_hasTriggered == false)
        {
            if (m_crystalLinks != null && m_crystalLinks.Count > 0)
            {
                if (CheckCrystals() == true && AreRequiredObjectivesComplete(WaveGun.Instance))
                {
                    PlayCompleteActions(WaveGun.Instance);
                    m_hasTriggered = true;
                }
            }
        }

        if(m_hasTriggered && m_death_particle!=null && !m_awsome_timer.IsComplete()) {
            m_awsome_timer.Update();
            m_particle_isnt = (GameObject)Instantiate(m_death_particle,transform.position,transform.rotation);
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
        for (int i = 0; i < m_crystalLinks.Count; i++)
        {
            WaveCrystal crystalLink = m_crystalLinks[i];
            crystalLink.GetComponent<Collider>().enabled = false;
            WaveGun.Instance.m_wave_target = null;
        }

        if(!m_has_played_audio) {
            m_has_played_audio = true;
            GetComponent<AudioSource>().PlayOneShot(m_waah);
        }


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
    protected void CompleteObjectives(WaveGun player)
    {
        for (int i = 0; i < m_completesObjectives.Count; i++)
        {
            Objective currentObjective = m_completesObjectives[i];
            player.CompleteObjective(currentObjective);
            if (CenterGem.Instance != null)
            {
                CenterGem.Instance.ObjectiveCompleted(currentObjective.m_objectiveID);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        ActOnCollision(other);
    }

    protected void ActOnCollision(Collider other)
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

