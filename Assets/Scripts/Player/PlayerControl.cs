using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Float
    [SerializeField]
    private float normalSpeed;
    private float speed;
    //Int
    private int dir = 2;
    //Vector
    private Vector2 direction;
    private Vector2 directionDodge = new Vector2();
    //Bool
    private bool canRun = true;
    private bool inDodge;
    //Others
    private Rigidbody2D rb;
    private BattleControl bc;
    private Animator myAnimator;

    void Start()
    {
        setNormalSpeed(3);
        myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BattleControl>();
    }

    void FixedUpdate()
    {
        Inputs();
        Movement();
    }
    void Movement()
    { //A movimentação do player em si
        if (Input.GetKey(KeybindScript.Instace.WorldKeys["Sprint"]) && canRun)
        {
            speed = normalSpeed * 2;
        }
        else
        {
            speed = normalSpeed;
        }
        rb.velocity = direction.normalized * speed;
        if (IsMoving)
        {
            Animate();
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
    void Inputs()
    { //Verifica a direção que o player deverá ir
        direction = Vector2.zero;
        if (Input.GetKey(KeybindScript.Instace.WorldKeys["Up"]))
        {
            direction += Vector2.up;
            directionDodge = new Vector2(0, 1);
            dir = 0;
        }
        if (Input.GetKey(KeybindScript.Instace.WorldKeys["Left"]))
        {
            direction += Vector2.left;
            directionDodge = new Vector2(-1, 0);
            dir = 1;
        }
        if (Input.GetKey(KeybindScript.Instace.WorldKeys["Down"]))
        {
            direction += Vector2.down;
            directionDodge = new Vector2(0, -1);
            dir = 2;
        }
        if (Input.GetKey(KeybindScript.Instace.WorldKeys["Right"]))
        {
            direction += Vector2.right;
            directionDodge = new Vector2(1, 0);
            dir = 3;
        }
        if (bc.enabled)
        {
            bc.setDirection(direction);
            bc.SetPointIndex(dir);
        }
    }
    void Animate()
    {
        myAnimator.SetLayerWeight(1, 1);
        myAnimator.SetFloat("x", direction.x);
        myAnimator.SetFloat("y", direction.y);
    }
    public void setNormalSpeed(float normalSpeed)
    {
        this.normalSpeed = normalSpeed;
    }
    public void setCanRun(bool canRun)
    {
        this.canRun = canRun;
    }
    public void setInDodge(bool inDodge)
    {
        this.inDodge = inDodge;
    }

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }
}
