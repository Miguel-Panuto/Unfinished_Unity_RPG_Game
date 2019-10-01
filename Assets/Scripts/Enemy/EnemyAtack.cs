using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    [SerializeField]
    private float damage;
    private Vector2 direction;

    void Update()
    {
        direction = GameObject.Find("Enemy").GetComponent<Enemy>().Direction;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Attributes>().TakeDamage(damage, direction);
        }
    }
}
