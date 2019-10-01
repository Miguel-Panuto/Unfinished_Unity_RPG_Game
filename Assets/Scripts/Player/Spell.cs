using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField]
    private float damage;
    [SerializeField]
    private bool isEnd = false;
    private Vector3 target;
    private float spellLifeTime = 3;
    private Collider2D col;
    private Animator animator;
    [SerializeField]
    private float setVelocity;
    [SerializeField]
    private bool canGo = false;
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        damage *= GameObject.Find("PlayerFX").GetComponent<BattleControl>().DamageMult;
        animator.SetBool("DestroySpell", false);
        setVelocity = 1;
    }

    void Update()
    {
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        animator.SetBool("CanGo", canGo);

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.Translate(Vector2.right * setVelocity * Time.deltaTime);
        spellLifeTime -= Time.deltaTime;
        if (spellLifeTime <= 0)
        {
            animator.SetBool("DestroySpell", true);
        }
        if(isEnd)
        {
            Destroy(gameObject);
        }         
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Dano recebido: " + damage);
            other.GetComponent<Enemy>().TakeDamage(damage);
            animator.SetBool("DestroySpell", true);
        }
    }
}
