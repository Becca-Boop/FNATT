using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Text;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ShiftTimer : MonoBehaviour
{
    [SerializeField] private float Timer;
    [SerializeField] private int ShiftEndTime = 6;
    [SerializeField] private int night;
    [SerializeField] private string DigitalClock;
    [SerializeField] private float TimeMultiplier = 1f;
    [SerializeField] private TextMeshProUGUI ClockText;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] public GameObject DeathScreen;
    [SerializeField] public bool Won = false;
    [SerializeField] public bool Dead = false;
    [SerializeField] public bool written = false;
    [SerializeField] public float deadtime = 3;
    [SerializeField] private AnimatronicSystem[] Animatronics;
    [SerializeField] private PowerSystem Power;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DigitalClock = "";

        if (MainManager.Instance != null)
        {
            night = MainManager.Instance.night;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime * TimeMultiplier;
        if (!Won && !Dead)
        {          
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
        else if (Won && !written)
        {
            CreateText(); 
            Timer = 0;
            written = true;      
        }
        else if (Won && Timer >= 2)
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    void CreateText() {
        string path = Application.dataPath + "/Resources/data.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "");
        }
        else
        {
            var contents = File.ReadAllLines(path);
                          
            StringBuilder sb = new StringBuilder(contents[0]);
            StringBuilder sb2 = new StringBuilder(contents[1]);
            StringBuilder sb3 = new StringBuilder(contents[2]);
            StringBuilder sb4 = new StringBuilder(contents[3]);
            int index = contents[0].IndexOf("=");

            // if ((int)contents[0][index+2] < night+2)
            // {
                string N = (night+2).ToString();
                sb[index+2] = N[0];
            //}
            if (night>3)
            {
                if (night == 4)
                {                    
                    index = contents[1].IndexOf("=");
                    sb2[index+2] = 't';
                    sb2[index+3] = 'r';
                    sb2[index+4] = 'u';
                    sb2[index+5] = 'e';
                    if (sb2[index+6] != ' ')
                    {
                        sb2[index+6] = ' ';
                    }
                }
                if (night == 5)
                {                    
                    index = contents[2].IndexOf("=");
                    sb3[index+2] = 't';
                    sb3[index+3] = 'r';
                    sb3[index+4] = 'u';
                    sb3[index+5] = 'e';
                    if (sb3[index+6] != ' ')
                    {
                        sb3[index+6] = ' ';
                    }
                }
                if (night == 6)
                {                    
                    index = contents[3].IndexOf("=");
                    sb4[index+2] = 't';
                    sb4[index+3] = 'r';
                    sb4[index+4] = 'u';
                    sb4[index+5] = 'e';
                    if (sb4[index+6] != ' ')
                    {
                        sb4[index+6] = ' ';
                    }
                }
            }
            string str2 = sb2.ToString();
            string str3 = sb3.ToString();
            string str4 = sb4.ToString();
            
            if (str2.EndsWith(" "))
            {
                str2 = str2.Remove(str2.Length-1);
            }
            if (str3.EndsWith(" "))
            {
                str3 = str3.Remove(str3.Length-1);
            }
            if (str4.EndsWith(" "))
            {
                str4 = str4.Remove(str4.Length-1);
            }

            string file = sb + "\n" + str2 + "\n" + str3 + "\n" + str4 + "\nplaynight = 0";

            File.WriteAllText(path, file);
        }
    }

}
