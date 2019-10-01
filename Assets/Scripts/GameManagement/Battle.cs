using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Battle : MonoBehaviour
{

    private PlayerControl mc;
    private BattleControl bc;
    private GameObject player;
    private AIPath aipath;
    void Start()
    {
        player = GameObject.Find("PlayerFX");
        if (GameObject.FindWithTag("Enemy") != null)
        {
            aipath = GameObject.FindWithTag("Enemy").GetComponent<AIPath>();
            aipath.canMove = false;
        }
        else
        {
            aipath = null;
        }
        mc = player.GetComponent<PlayerControl>();
        bc = player.GetComponent<BattleControl>();
    }

    public void battleValidade(bool isBattleStarted)
    {
        if (isBattleStarted)
        {
            battleStarted();
        }
        else
        {
            battleEnded();
        }
    }
    void battleStarted()
    {
        mc.setNormalSpeed(2f);
        mc.setCanRun(false);
        aipath.canMove = true;
        bc.enabled = true;
    }
    void battleEnded()
    {
        mc.enabled = true;
        mc.setNormalSpeed(3f);
        mc.setCanRun(true);
        bc.SetParticles(false);
        bc.enabled = false;
        aipath.canMove = false;
        bc.setInDodgeToFalse();
    }
}
