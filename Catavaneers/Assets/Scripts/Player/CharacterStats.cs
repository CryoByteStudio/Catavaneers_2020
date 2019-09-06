using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharacterStats : MonoBehaviour
{

    [Range(10.0f, 30.0f)]  public float speed_mod_fl = 10f;
    [Range(0.0f, 200.0f)] public float maxHP_fl = 100f;
    [Range(0.0f, 100.0f)] public float damageTaken_fl = 0f;
    [Range(0.0f, 100.0f)] public float damage_healed_fl = 0f;
    [SerializeField] private float current_HP_fl = 0f;
    private Character_control current_char;

    public int level = 0;
    public float str = 0;
    public float speed = 0;
    public float hp = 0;
    public float sp = 0;

    public GameObject statMenuUI;
    public static bool statMenuOpen = false;

    private float tempHP;
    public Text statPoints;
    public Text strText;
    public Text speedText;
    public Text hpText;

    // Start is called before the first frame update
    void Start()
    {

        // At the start, it will change the game objects speed to speed mod.
        current_char = GetComponent<Character_control>();
        
        current_HP_fl = maxHP_fl;
        setTexts();
    }

    // Update is called once per frame
    void Update()
    {
        current_char.speed_fl = speed_mod_fl;
        // Currently HP is tied to game model size.

        StatMod();
        transform.localScale = new Vector3(current_HP_fl / 100, current_HP_fl / 100, current_HP_fl / 100);

        if (Input.GetKeyDown(KeyCode.P))
        {
            level++;
            sp++;
            setTexts();
        }



        // Section dedicated to incrementing stats (only functions if SP is > 0)

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            if(statMenuOpen)
            {
                if(sp > 0)
                {
                    sp--;
                    str++;
                    maxHP_fl += 1;
                    setTexts();
                }
              
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (statMenuOpen)
            {
                if(sp > 0)
                {
                    sp--;
                    speed++;
                    current_char.speed_fl = speed_mod_fl + speed;
                    setTexts();
                }

            }

        }

        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            if (statMenuOpen)
            {
                if(sp > 0)
                {
                    sp--;
                    hp++;
                    setTexts();
                }
            }

        }

        // Input for opening / closing stat menu

        if (Input.GetKeyDown(KeyCode.O))
        {
           if(statMenuOpen)
            {

                CloseStats();
            } else
            {
                OpenStats();

            }
        }

    }

    void setTexts()
    {
        statPoints.text = "SP: " + sp.ToString();
        strText.text = "Strength : " + str.ToString();
        speedText.text = "Speed : " + speed.ToString();
        hpText.text = "HP : " + hp.ToString();
    }


    private void StatMod()
    {

        // Hp calculation here
        tempHP = maxHP_fl - damageTaken_fl + damage_healed_fl;
        if (tempHP > maxHP_fl)
            current_HP_fl = 200.0f;
        else
            current_HP_fl = maxHP_fl - damageTaken_fl + damage_healed_fl;

    }

    public void ModifySpeed(float speed_fl)
    {
        speed_mod_fl += speed_fl;
    }

    void OpenStats()
    {
        statMenuUI.SetActive(true);
        statMenuOpen = true;
    }

    void CloseStats()
    {
        statMenuUI.SetActive(false);
        statMenuOpen = false;  
    }


}


