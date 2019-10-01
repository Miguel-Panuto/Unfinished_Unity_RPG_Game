using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{

    private Battle battle;
    private Enemy parent;
    void Start()
    {
        parent = GetComponentInParent<Enemy>();
        battle = GameObject.Find("GameManager").GetComponent<Battle>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            battle.battleValidade(true);
            parent.Target = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            battle.battleValidade(false);
            parent.Target = null;
        }
    }
}
