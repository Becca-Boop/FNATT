using TMPro;
using UnityEngine;

public class ShiftTimer : MonoBehaviour
{
    [SerializeField] private float Timer;
    [SerializeField] private int ShiftEndTime = 6;
    [SerializeField] private string DigitalClock;
    [SerializeField] private float TimeMultiplier = 1f;
    [SerializeField] private TextMeshProUGUI ClockText;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] public GameObject DeathScreen;
    [SerializeField] public bool Won = false;
    [SerializeField] public bool Dead = false;
    [SerializeField] public float deadtime = 3;
    [SerializeField] private AnimatronicSystem[] Animatronics;
    [SerializeField] private PowerSystem Power;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DigitalClock = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!Won && !Dead)
        {
            Timer += Time.deltaTime * TimeMultiplier;

            var hours = Mathf.FloorToInt(Timer / 60);
            var minutes = Mathf.FloorToInt(Timer - hours * 60);

            if (minutes == 0)
            {
                for (int i = 0; i < Animatronics.Length; i++)
                {
                    Animatronics[i].ChangeAggByHour(hours);
                }
            }

            if (hours >= ShiftEndTime)
            {
                WinScreen.SetActive(true);
                Won = true;
            }

            if (hours == 0)
            {
                hours = 12;
            }

            DigitalClock = string.Format("{0:00}AM", hours);
            ClockText.text = DigitalClock;   
        }
        else if (Dead)
        {
            ClockText.enabled = false;
            Power.PowerText.enabled = false;
            if (deadtime <= 0f)
            {
                DeathScreen.SetActive(true);
            }   
            else
            {
                deadtime -= Time.deltaTime;
            }         
        }
    }
}
