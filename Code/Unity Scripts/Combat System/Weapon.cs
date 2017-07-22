using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int attackPower;

    void OnCollisionEnter(Collision collision)
    {
        CombatData enemyData = collision.gameObject.GetComponent<CombatData>();
        enemyData.hp -= Mathf.Clamp(attackPower - enemyData.defensePower, 0, attackPower);
    }
}