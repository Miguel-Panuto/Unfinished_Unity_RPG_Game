using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour
{

    private bool isAtacked = true;
    private int dice;
    private Vector2 direction;
    private Rigidbody2D rb;
    private PlayerControl mc;
    private bool inDodge;
    private float startDodgeTime = 0.1f;
    private float dodgeTime;

    [SerializeField]
    private GameObject[] spellPrefabs;

    private float damageMult;
    private Attributes att;
    private float intensity;
    private bool powerSkill;

   [SerializeField]
   private GameObject particles;

    private int pointIndex;

    void Start()
    {
        powerSkill = false;
        rb = GetComponent<Rigidbody2D>();
        mc = GetComponent<PlayerControl>();
        att = GameObject.Find("Player").GetComponent<Attributes>();
        inDodge = false;
        dodgeTime = startDodgeTime;
    }

    void Update()
    { 
        if (Input.GetKeyDown(KeybindScript.Instace.BattleKeys["BTLRolldice"]) && isAtacked)
        {
            dice = Random.Range(1, 20);
            isAtacked = false;
        }
        if (Input.GetKeyDown(KeybindScript.Instace.BattleKeys["BTLAtack"]) && !isAtacked)
        {
            if (powerSkill && att.GetMana() >= 20)
            {
                isAtacked = true;
                Atack(2);
                att.UseMana(20);
            }
            else if (!powerSkill)
            {
                isAtacked = true;
                Atack(1);
            }
            else
            {
                Debug.Log("Mana insuficiente");
            }

        }
        if (Input.GetKeyDown(KeybindScript.Instace.BattleKeys["BTLDodge"]) && !inDodge && att.GetMana() >= 10)
        {
            inDodge = true;
            Intensity = 10;
            att.UseMana(10);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            powerSkill = !powerSkill;
            SetParticles(powerSkill);
        }
        if (inDodge)
        {
            Dodge(intensity);
        }
        mc.setInDodge(inDodge);
    }
    void Atack(float atackIntensity)
    {
        if (dice == 1)
        {
            Debug.Log("Atack missed");
        }
        else if (dice > 1 && dice < 6)
        {
            CastSpell(0.2f * atackIntensity);
        }
        else if (dice >= 6 && dice < 10)
        {
            CastSpell(0.5f * atackIntensity);
        }
        else if (dice >= 10 && dice < 15)
        {
            CastSpell(1f * atackIntensity);
        }
        else if (dice >= 15 && dice < 20)
        {
            CastSpell(1.5f * atackIntensity);
        }
        else if (dice == 20)
        {
            CastSpell(2f * atackIntensity);
        }
    }
    void Dodge(float intensity)
    {
        if (dodgeTime <= 0)
        {
            setInDodgeToFalse();
            rb.velocity = Vector2.zero;
            mc.enabled = true;
        }
        else
        {
            mc.enabled = false;
            dodgeTime -= Time.deltaTime;
            transform.Translate(direction * intensity * Time.deltaTime);
        }
    }

    private void CastSpell(float damageMult)
    {
        this.damageMult = damageMult;
        Instantiate(spellPrefabs[0], transform.position, Quaternion.identity);
    }
    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }
    public void setInDodgeToFalse()
    {
        inDodge = false;
        dodgeTime = startDodgeTime;
    }
    public void setInDodgeToTrue(Vector2 direction)
    {
        inDodge = true;
        this.direction = direction.normalized;
    }
    public float DamageMult
    {
        get
        {
            return damageMult;
        }
    }
    public float Intensity
    {
        set
        {
            intensity = value;
        }
        get
        {
            return Intensity;
        }
    }
    public void SetPointIndex(int num)
    {
        pointIndex = num;
    }
    public void SetParticles(bool isActive)
    {
        particles.SetActive(isActive);
    }
}
