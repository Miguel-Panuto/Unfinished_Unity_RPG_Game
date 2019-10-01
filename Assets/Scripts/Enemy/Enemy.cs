using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    [SerializeField]
    private GameObject player;
    private Transform target;
    private IState currentState;
    private Vector2 direction;
    private Animator animator;
    [SerializeField]
    private GameObject bloodParticles;
    public Vector2 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }

    public Transform Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    void Awake()
    {
        ChangeState(new IdleState());
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
        currentState.Update();
        FollowTarget();
        if (IsMoving)
        {
            SetLayerActive("Walk");
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
        }
        else
        {
            SetLayerActive("Idle");
        }
    }
    private void FollowTarget()
    {
        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
        }
        else
        {
            SetLayerActive("Idle");
        }
    }
    private void SetLayerActive(string layer)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }
        animator.SetLayerWeight(animator.GetLayerIndex(layer), 1);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;

        currentState.Enter(this);
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(bloodParticles, transform.position, Quaternion.identity);
    }
}
