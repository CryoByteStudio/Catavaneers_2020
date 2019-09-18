using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharacterStats : MonoBehaviour
{
    private Character_control current_char;
    private Health playerHP;
    private Player_Inventory playerInventory;
    private Character_interaction playerDamage;
    [SerializeField] string horizontal_ctrl_str = "Horizontal_P1"; //replace P1 in inspecter with P2, P3, P4 acordingly
    [SerializeField] string interact_botton_str = "Primary_interact_P1"; //replace P1 in inspecter with P2, P3, P4 acordingly


    public int level = 0;
    public float str = 0;
    public float speed = 0;
    public float sp = 0;

    public GameObject statMenuUI;

    public Button firstButton;
    public Button secondButton;
    public Button thirdButton;
    private int buttonChoice;



    public bool statMenuOpen = false;
    public float tempSpeed = 0;



    Rigidbody RB;

    public Text statPoints;
    public Text strText;
    public Text speedText;
    public Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        current_char = GetComponent<Character_control>();
        playerHP = GetComponent<Health>();
        playerInventory = GetComponent<Player_Inventory>();
        playerDamage = GetComponent<Character_interaction>();
        tempSpeed = current_char.speed_fl;

        setTexts();
    }

    // Update is called once per frame
    void Update()
    {


        if(statMenuOpen)
        {
            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw(horizontal_ctrl_str) > 0)
            {
                if (buttonChoice == 2)
                    buttonChoice = 0;
                else
                    buttonChoice++;

                Debug.Log("Button Choice is" + buttonChoice);
            }

            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw(horizontal_ctrl_str) < 0)
            {
                if (buttonChoice == 0)
                    buttonChoice = 2;
                else
                    buttonChoice--;

                Debug.Log("Button Choice is" + buttonChoice);
            }

            menuCheck(buttonChoice);

        }
        

            // Currently HP is tied to game model size.
            //tempSpeed = current_char.speed_fl;

            if (Input.GetKeyDown(KeyCode.P))
        {
            level++;
            sp++;
            setTexts();
        }

        // Input for opening / closing stat menu

        if (Input.GetKeyDown(KeyCode.Q))
        {
           if(statMenuOpen)
            {
                CloseStats();
                
            } else
            {
                Debug.Log("Stats Opened, player frozen");
                OpenStats();      
            }
        }
    }

    void setTexts()
    {
        statPoints.text = "SP: " + sp.ToString();
        strText.text = "Strength : " + str.ToString();
        speedText.text = "Speed : " + current_char.speed_fl.ToString();
        hpText.text = "HP : " + playerHP.maxHealth.ToString();
    }


    void OpenStats()
    {
        buttonChoice = 0;
        statMenuUI.SetActive(true);
        RB.constraints = RigidbodyConstraints.FreezeAll;
        current_char.speed_fl = 0f;
        statMenuOpen = true;
    }

    void CloseStats()
    {
        RB.constraints = RigidbodyConstraints.None;
        current_char.speed_fl = tempSpeed;
        statMenuUI.SetActive(false);
        statMenuOpen = false;  
    }


    public void addStr()
    {
        if (statMenuOpen)
        {
            if (sp > 0)
            {
                sp--;
                str++;
                playerDamage.damage_fl += str;
                setTexts();
            }

        }
    }

    public void addAgi()
    {
        if (statMenuOpen)
        {
            if (sp > 0)
            {
                sp--;
                current_char.speed_fl++;
                tempSpeed = current_char.speed_fl;
                current_char.speed_fl = 0f;
                setTexts();
            }
        }
    }
    
    public void addHP()
    {
        if (statMenuOpen)
        {
            if (sp > 0)
            {
                sp--;
                playerHP.maxHealth += 1;
                setTexts();
            }
        }
    }
       
    public void ModifySpeed(float speed_fl)
    {
        current_char.speed_fl += speed_fl;
    }

    void menuCheck(int choice)
    {
        if (Input.GetButtonDown(interact_botton_str))
        {
            ActivateButton(choice);
        }


        if (choice == 0)
        {
            TurnRed();
            TurnWhite(choice);
        }
        if(choice == 1)
        {
            TurnGreen();
            TurnWhite(choice);
        }
        if(choice == 2)
        {
            TurnYellow();
            TurnWhite(choice);
        }
    }

    public void TurnRed()
    {
        ColorBlock colors = firstButton.colors;
        colors.normalColor = Color.red;
        firstButton.colors = colors;
    }

    public void TurnGreen()
    {
        ColorBlock colors = secondButton.colors;
        colors.normalColor = Color.green;
        secondButton.colors = colors;
    }

    public void TurnYellow()
    {
        ColorBlock colors = thirdButton.colors;
        colors.normalColor = Color.yellow;
        thirdButton.colors = colors;
    }

    public void TurnWhite(int select)
    {
        ColorBlock tempColor1 = firstButton.colors;
        ColorBlock tempColor2 = secondButton.colors;
        ColorBlock tempColor3 = thirdButton.colors;
        tempColor1.normalColor = Color.white;
        tempColor2.normalColor = Color.white;
        tempColor3.normalColor = Color.white;

        if(select == 0)
        {
            secondButton.colors = tempColor2;
            thirdButton.colors = tempColor3;
        }
        if (select == 1)
        {
            firstButton.colors = tempColor2;
            thirdButton.colors = tempColor3;
        }
        if (select == 2)
        {
            firstButton.colors = tempColor2;
            secondButton.colors = tempColor3;
        }

    }

    void ActivateButton(int choice)
    {
        if(choice == 0)
        {
            firstButton.onClick.Invoke();
        }
        if (choice == 1)
        {
            secondButton.onClick.Invoke();
        }
        if (choice == 2)
        {
            thirdButton.onClick.Invoke();
        }
    }



    void weaponSelect()
    {

    }


















}


