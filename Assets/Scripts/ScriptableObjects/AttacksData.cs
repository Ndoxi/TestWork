using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/AttacksData")]
public class AttacksData : ScriptableObject
{
    public AttackData[] Attacks;

    public AttackData GetAttackData(PlayerAttackController.AttackType type)
    {
        foreach (var item in Attacks)
        {
            if (item.AttackType == type)
                return item;
        }
        return default;
    }
}

[System.Serializable]
public struct AttackData
{
    public PlayerAttackController.AttackType AttackType;
    public float AttackRange;
    public float AttackSpeed;
    public float Damage;
    public string AnimatorTag;
}
