using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cycle_Manager : MonoBehaviour
{
    [SerializeField] private Text timer_text;
    [SerializeField] private Text day_counter_text;
    [SerializeField] private float day_timer_float;
    [SerializeField] private float night_timer_float;
    [SerializeField] private bool is_day;
    [SerializeField] private bool is_night;
    [SerializeField] private int current_day_int;
    [SerializeField] private int max_day_int;

    private float timer_float;
    private bool is_timer_counting;
    private bool is_done_counting;

    void Start()
    {
        current_day_int = 1;
        timer_float = day_timer_float;
        StartDayCycle();

    }

    void Update()
    {
        day_counter_text.text = current_day_int.ToString();
        if(timer_float >= 0.0f && is_timer_counting)
        {
            timer_float -= Time.deltaTime;
            timer_text.text = timer_float.ToString();
        }
        else if(timer_float <= 0.0f)
        {
            is_done_counting = true;
            is_timer_counting = false;
            timer_text.text = "0.00";
            timer_float = 0.0f;
            if (is_day)
            {
                StartNightCycle();
            }
            else
            {
                current_day_int++;
                StartDayCycle();
            }
        }

    }

    void StartDayCycle()
    {
        is_day = true;
        is_night = false;
        is_timer_counting = true;
    }

    void PauseCycle()
    {
        is_timer_counting = false;
    }

    void ResumeCycle()
    {
        is_timer_counting = true;
    }

    void StartNightCycle()
    {
        is_day = false;
        is_night = true;
    }
}
