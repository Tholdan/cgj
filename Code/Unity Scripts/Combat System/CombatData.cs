using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatData : MonoBehaviour {

    //PUBLIC
    public int hp;
    public int defensePower;
    public GameObject weapon;

    //PRIVATE
    private int currentHp;

    void Start()
    {
        this.currentHp = this.hp;
    }
}