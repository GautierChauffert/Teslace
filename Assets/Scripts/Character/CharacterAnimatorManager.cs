using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterAnimatorManager : MonoBehaviour {
    public CharacterColorAnimators[] characterAnimators;
    private CharacterColorAnimators selectedCharacterAnimator;
    private CharacterColor color;
    private int actualDamageLevel;

    private Health m_health;
    private Animator m_animator;
    private PlayerCharacteristics playerCharacteristics;
    // Use this for initialization
    void Start () {
        m_health = GetComponent<Health>();
        playerCharacteristics = transform.parent.GetComponent<PlayerCharacteristics>();
        SetSelectedCharacterAnimator(playerCharacteristics.color);
        m_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerCharacteristics != null && playerCharacteristics.color != color)
        {
            color = playerCharacteristics.color;
            SetSelectedCharacterAnimator(color);
            UpdateAnimator();
        }
        if (m_health != null && actualDamageLevel != m_health.DamageLevel)
        {
            actualDamageLevel = m_health.DamageLevel;
            UpdateAnimator();
        }

	}

    void UpdateAnimator()
    {
        if (m_animator != null && selectedCharacterAnimator != null)
            m_animator.runtimeAnimatorController = selectedCharacterAnimator.animatorDamageLevel[actualDamageLevel];
    }
    void SetSelectedCharacterAnimator(CharacterColor color)
    {
        selectedCharacterAnimator = characterAnimators.Where(c => c.characterColor == color).FirstOrDefault();
    }
}

[System.Serializable]
public class CharacterColorAnimators
{
    public CharacterColor characterColor;
    public RuntimeAnimatorController[] animatorDamageLevel;
}
