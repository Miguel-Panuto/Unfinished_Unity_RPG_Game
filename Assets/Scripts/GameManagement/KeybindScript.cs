using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeybindScript : MonoBehaviour
{
    private static KeybindScript instace;

    public static KeybindScript Instace
    {
        get
        {
            if (instace == null)
            {
                instace = FindObjectOfType<KeybindScript>();
            }
            return instace;
        }
    }
    public Dictionary<string, KeyCode> WorldKeys { get; private set; } = new Dictionary<string, KeyCode>();
    public Dictionary<string, KeyCode> BattleKeys { get; private set; } = new Dictionary<string, KeyCode>();
    [SerializeField]
    private TextMeshProUGUI up, left, down, right, rolldice, atack, sprint, dodge, powerSkill;

    private Color32 normal = new Color32(255, 255, 255, 255);
    private Color32 selected = new Color32(130, 130, 130, 255);
    private GameObject currentKey;
    void Start()
    {
        WorldKeys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        WorldKeys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        WorldKeys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        WorldKeys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        WorldKeys.Add("Sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift")));
        BattleKeys.Add("BTLRolldice", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BTLRolldice", "Mouse1")));
        BattleKeys.Add("BTLAtack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BTLAtack", "Mouse0")));
        BattleKeys.Add("BTLDodge", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BTLDodge", "Space")));
        BattleKeys.Add("BTLPowerSkill", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BTLPowerSkill", "LeftShift")));

        SetAllTexts();

    }

    private void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if (currentKey.name.Contains("BTL"))
                {
                    VerifyBattleKeys(e.keyCode);
                    BattleKeys[currentKey.name] = e.keyCode;
                }
                else
                {
                    VerifyWorldKeys(e.keyCode);
                    WorldKeys[currentKey.name] = e.keyCode;
                }
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
            else if (e.isMouse)
            {
                KeyCode mouseButtonKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse" + e.button);
                if (currentKey.name.Contains("BTL"))
                {
                    VerifyBattleKeys(mouseButtonKey);
                    BattleKeys[currentKey.name] = mouseButtonKey;
                }
                else
                {
                    VerifyWorldKeys(mouseButtonKey);
                    WorldKeys[currentKey.name] = mouseButtonKey;
                }
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mouseButtonKey.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {

        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    public void SaveKeys()
    {
        foreach (var key in WorldKeys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        foreach (var key in BattleKeys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
    }

    public void DefaulKeys()
    {
        PlayerPrefs.DeleteAll();

        WorldKeys["Up"] = KeyCode.W;
        WorldKeys["Down"] = KeyCode.S;
        WorldKeys["Left"] = KeyCode.A;
        WorldKeys["Right"] = KeyCode.D;
        WorldKeys["Sprint"] = KeyCode.LeftShift;
        BattleKeys["BTLRolldice"] = KeyCode.Mouse1;
        BattleKeys["BTLAtack"] = KeyCode.Mouse0;
        BattleKeys["BTLDodge"] = KeyCode.Space;
        BattleKeys["BTLPowerSkill"] = KeyCode.LeftShift;

        SetAllTexts();
    }

    void SetAllTexts()
    {
        SetWorldKeysTexts();
        SetBattleKeysTexts();
    }
    void SetWorldKeysTexts()
    {
        up.text = WorldKeys["Up"].ToString();
        down.text = WorldKeys["Down"].ToString();
        left.text = WorldKeys["Left"].ToString();
        right.text = WorldKeys["Right"].ToString();
        sprint.text = WorldKeys["Sprint"].ToString();
    }
    void SetBattleKeysTexts()
    {
        rolldice.text = BattleKeys["BTLRolldice"].ToString();
        atack.text = BattleKeys["BTLAtack"].ToString();
        dodge.text = BattleKeys["BTLDodge"].ToString();
        powerSkill.text = BattleKeys["BTLPowerSkill"].ToString();
    }
    void VerifyWorldKeys(KeyCode keyToCheck)
    {
        int i = 0;
        if(WorldKeys["Up"] == keyToCheck)
        {
            WorldKeys["Up"] = KeyCode.None;
            i++;
        }
        if(WorldKeys["Down"] == keyToCheck)
        {
            WorldKeys["Down"] = KeyCode.None;
            i++;
        }
        if(WorldKeys["Left"] == keyToCheck)
        {
            WorldKeys["Left"] = KeyCode.None;
            i++;
        }
        if(WorldKeys["Right"] == keyToCheck)
        {
            WorldKeys["Right"] = KeyCode.None;
            i++;
        }
        if(WorldKeys["Sprint"] == keyToCheck)
        {
            WorldKeys["Sprint"] = KeyCode.None;
            i++;
        }
        if(i > 0)
        {
            SetWorldKeysTexts();
        }
    }
    void VerifyBattleKeys(KeyCode keyToCheck)
    {
        int i = 0;
        if (BattleKeys["BTLAtack"] == keyToCheck)
        {
            BattleKeys["BTLAtack"] = KeyCode.None;
            i++;
        }
        if (BattleKeys["BTLRolldice"] == keyToCheck)
        {
            BattleKeys["BTLRolldice"] = KeyCode.None;
            i++;
        }
        if (BattleKeys["BTLDodge"] == keyToCheck)
        {
            BattleKeys["BTLDodge"] = KeyCode.None;
            i++;
        }
        if (BattleKeys["BTLPowerSkill"] == keyToCheck)
        {
            BattleKeys["BTLPowerSkill"] = KeyCode.None;
            i++;
        }
        if(i > 0)
        {
            SetBattleKeysTexts();
        }
    }
}
