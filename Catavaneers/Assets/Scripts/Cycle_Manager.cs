using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cycle_Manager : MonoBehaviour
{
    #region variables
    [SerializeField] private Text timer_text;
    [SerializeField] private Text day_counter_text;
    [SerializeField] private float day_timer_float;
    [SerializeField] private float night_timer_float;
    [SerializeField] public bool is_day;
    [SerializeField] private bool is_night;
    [SerializeField] private int current_day_int;
    [SerializeField] private int max_day_int;
    [SerializeField] private int wood_count;
    [SerializeField] private bool is_caravan_whole;
    [SerializeField] private bool has_caravan_travelled;
    [SerializeField] private float end_distance_float =200f;
    [SerializeField] private float dist_travelled_float = 0f;

    private float timer_float;
    private bool is_timer_counting;
    private bool is_done_counting;
    public Caravan caravan_s;
    PlayerAI[] playerAIs;
    public SpawnManager spawn_manager;
    public Caravan_Inventory caravan_inv;
    #endregion


    void Start()
    {
        current_day_int = 1;
        timer_float = day_timer_float;
        StartDayCycle();
        if(!spawn_manager)
        spawn_manager = FindObjectOfType<Caravan>().GetComponentInChildren<SpawnManager>();
    }

    void Update()
    {
        if (is_day)
            day_counter_text.text = "Day: " + current_day_int.ToString();
        else
            day_counter_text.text = "Night: " + current_day_int.ToString();
        if (timer_float >= 0.0f && is_timer_counting)
        {
            timer_float -= Time.deltaTime;
            //timer_text.text = timer_float.ToString();
            timer_text.text = Mathf.FloorToInt(timer_float / 60).ToString()+ "m : " + Mathf.FloorToInt(timer_float % 60).ToString() + "s";
        }
        else if(timer_float <= 0.0f && (is_day || is_night))
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
        if(caravan_s.parts_tf.Count == 12)
        {
            is_caravan_whole = true;
        }
        if (is_day && is_caravan_whole && wood_count >= 100 && !has_caravan_travelled && is_timer_counting)
        {
            
            PauseCycle();

            Debug.Log("caravan traveled");

        }else if (is_day && is_caravan_whole && wood_count >= 100 && !has_caravan_travelled && !is_timer_counting)
        {
            MoveCaravan();
        }
        else if (is_day && is_caravan_whole && has_caravan_travelled && !is_timer_counting)
        {
            ResumeCycle();
        }
        // wood_count = (I got incomplete code, im not sure what was meant to be here) ~ silas

    }
    /*
		Purpose: Starts New Day Cycle.
		Effects: resets day/ night variables, sets timer to day, starts timer.
		Input/Output: N/A
		Global Variables Used:is_day, is_night, is_timer_counting, timer_float, has_caravan_travelled.
     */
    void StartDayCycle()
    {
        is_day = true;
        is_night = false;
        is_timer_counting = true;
        timer_float = day_timer_float;
        has_caravan_travelled = false;
        spawn_manager.SetIsNight(false);
        
    }

    /*
    Purpose: Pause Timer to travel caravan.
    Effects: sets is_timer_counting to false.
    Input/Output: N/A
    Global Variables Used: is_timer_counting.
    */
    void PauseCycle()
    {
        is_timer_counting = false;
        playerAIs = FindObjectsOfType<PlayerAI>();
        for (int i =0; i<4; i++)
        {
            playerAIs[i].IsAttached = true;
        //   playerAIs[i].AttachSelf(playerAIs[i].caravan_attach_point);
        }
        
    }

    /*
    Purpose: Resume timer after caravan travelled.
    Effects: Set is_timer_counting to true, set has_caravan_travelled to false.
    Input/Output: N/A
    Global Variables Used: is_timer_counting, has_caravan_travelled.
    */
    void ResumeCycle()
    {
        is_timer_counting = true;

        playerAIs = FindObjectsOfType<PlayerAI>();
        for (int i = 0; i < playerAIs.Length; i++)
        {
         //   playerAIs[i].DetachSelf();
            playerAIs[i].IsAttached = false;
        }
    }

    /*
    Purpose: Starts night sycle timer
    Effects: sets all day night variables to night, sets timer to night timer, starts timer, if final day stop TIME.
    Input/Output: N/A
    Global Variables Used: is_day, is_night, is_timer_counting, timer_float, night_timer_float, current_day_int, max_day_int.
    */
    void StartNightCycle()
    {
        is_day = false;
        is_night = true;
        is_timer_counting = true;
        timer_float = night_timer_float;
        if (is_night && current_day_int == max_day_int)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            //FindObjectOfType<Caravan>().GetComponentInChildren<SpawnManager>().spawning = true;
            FindObjectOfType<SpawnManager>().spawning = true;
            spawn_manager.SetIsNight(true);
        }

    }

    /*
    Purpose: Moves the caravan.
    Effects: move caravan to new location, removes wood from caravan.
    Input/Output: N/A
    Global Variables Used: wood_count, has_caravan_travelled.
    */
    private void MoveCaravan()
    {
        FindObjectOfType<Caravan>().transform.position = new Vector3(FindObjectOfType<Caravan>().transform.position.x + ((end_distance_float / 3) * (timer_float / day_timer_float)),
                                                                 FindObjectOfType<Caravan>().transform.position.y, FindObjectOfType<Caravan>().transform.position.z);
        has_caravan_travelled = true;
        wood_count -= 100;
    }
}
