using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStats : MonoBehaviour
{

    [Range(10.0f, 30.0f)]  public float speed_mod_fl = 10f;
    [Range(0.0f, 200.0f)] public float maxHP_fl = 100f;
    [Range(0.0f, 100.0f)] public float damageTaken_fl = 0f;
    [Range(0.0f, 100.0f)] public float damageHealed_fl = 0f;
    [SerializeField] private float currentHP_fl = 0f;
    [Range(0.0f, 100.0f)] public float rotateMod_fl = 0f;
    private CharacterControl current_char;

    private float tempHP;

    // Start is called before the first frame update
    void Start()
    {

        // At the start, it will change the game objects speed to speed mod.
        current_char = GetComponent<CharacterControl>();
        
        currentHP_fl = maxHP_fl;

    }

    // Update is called once per frame
    void Update()
    {
        current_char.speed_fl = speed_mod_fl;
        // Currently HP is tied to game model size.

        StatMod();
        transform.localScale = new Vector3(currentHP_fl / 100, currentHP_fl / 100, currentHP_fl / 100);
        rotateClockwise();



    }

    private void rotateClockwise()
    {
        transform.Rotate(Vector3.back, rotateMod_fl);

    }

    private void StatMod()
    {

        // Hp calculation here

        tempHP = maxHP_fl - damageTaken_fl + damageHealed_fl;
        if (tempHP > maxHP_fl)
            currentHP_fl = 200.0f;
        else
            currentHP_fl = maxHP_fl - damageTaken_fl + damageHealed_fl;
  

    }


}


