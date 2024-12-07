using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AnimatronicSystem : MonoBehaviour
{
    [SerializeField] private NavMeshAgent NMA;
    [SerializeField] private GameObject[] Targets;
    [SerializeField] private int CurrentTarget;
    [SerializeField] private float MinCoolDownTime;
    [SerializeField] private float MaxCoolDownTime;
    [SerializeField] private float CoolDownTimer;
    [SerializeField] private int MinChanceToMove = 1;
    [SerializeField] private int MaxChanceToMove = 20;
    [SerializeField] private int ThresholdToPass = 3;
    [SerializeField] private int[] AggressionByHour;
    [SerializeField] private int MinAggAdd = 2;
    [SerializeField] private int MaxAggAdd = 5;
    [SerializeField] private int HoursChanged;
    [SerializeField] private GameObject StageLight;
    [SerializeField] private GameObject CameraStatic;
    [SerializeField] private GameObject CameraSystemUI;
    [SerializeField] private float count;
    [SerializeField] private GameObject DoorLight;
    [SerializeField] private GameObject JumpscareLight;
    [SerializeField] private int Animatronic;
    [SerializeField] private AudioSource AtDoorSound;
    [SerializeField] private AudioSource RedLaugh;
    [SerializeField] private AudioSource JumpScareSound;
    [SerializeField] private bool soundPlayed;
    [SerializeField] private PowerSystem Power;
    [SerializeField] private ShiftTimer ShiftTimer;
    [SerializeField] private float PosX;
    [SerializeField] public float closecamstime;
    [SerializeField] public bool closecams;
    [SerializeField] public bool IsJumpscare;
    [SerializeField] public float RedLaughTime;
    [SerializeField] private CameraSystem CameraSystem;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NMA = GetComponent<NavMeshAgent>();
        CameraStatic.SetActive(false);
        soundPlayed = false;

        StageLightOn();
        JumpscareLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShiftTimer.Won && !ShiftTimer.Dead)
        {
            if(CurrentTarget != 0)
            {
                StageLightOff();
            }        

            if (Animatronic < 2 && Targets[CurrentTarget].GetComponent<DestinationPoint>().IsDoor && DoorLight.activeSelf == true && soundPlayed == false && Power.Power > 0f)
            {
                AtDoorSound.Play();
                soundPlayed = true;
                if (CoolDownTimer >= 0)
                {
                    CoolDownTimer += 10;
                }
                else
                {
                    CoolDownTimer = 10;
                }
            }
            else if (Animatronic == 2 && Targets[CurrentTarget].GetComponent<DestinationPoint>().IsDoor)
            {
                if (!soundPlayed)
                {
                    RedLaugh.Play();
                    soundPlayed = true;
                    RedLaughTime = UnityEngine.Random.Range(6, 20);
                }
                else
                {
                    if (RedLaughTime <= 0f)
                    {
                        soundPlayed = false;
                    }
                }
                RedLaughTime -= Time.deltaTime;
            }

            if (closecamstime <= 0f || (closecams && CameraSystemUI.activeSelf == false))
            {
                CameraSystemUI.SetActive(false);
                CameraStatic.SetActive(false);
                CurrentTarget = Targets.Length -1;
                JumpscareLight.SetActive(true);
                ShiftTimer.Dead = true;
                jumpscare();
            }


            if(CoolDownTimer <= 0)
            {
                var chanceCheck = UnityEngine.Random.Range(MinChanceToMove, MaxChanceToMove);            
                    
                if (chanceCheck <= ThresholdToPass)
                {                
                    if (Targets[CurrentTarget].GetComponent<DestinationPoint>().IsDoor)
                    {
                        if (Targets[CurrentTarget].GetComponent<DestinationPoint>().Door.IsOpen || Power.Power <= 0f)
                        {   
                            if (!closecams)
                            {
                                closecamstime = 10;
                            }
                            if (CameraSystemUI.activeSelf == true && Animatronic < 2)
                            {
                                closecams = true;
                            }
                            else if (Animatronic == 2 && CameraSystem.CurrentCam == 3 && CameraSystemUI.activeSelf == true)
                            {

                            }
                            else
                            {
                                CurrentTarget = Targets.Length -1;
                                JumpscareLight.SetActive(true);
                                ShiftTimer.Dead = true;
                                jumpscare();
                            }                     
                        }
                        else
                        {
                            CurrentTarget = 0;      
                            soundPlayed = false;   
                            CoolDownTimer = 15;
                            StageLightOn();                        
                        }
                    }          
                    else if (Targets[CurrentTarget].GetComponent<DestinationPoint>().IsOffice)
                    {
                        JumpscareLight.SetActive(true);
                    }      
                    else
                    {
                        CurrentTarget += 1;    
                        if (CurrentTarget == (Targets.Length-2))
                        {
                            CoolDownTimer = 15;  
                        }                                  
                        else if (CurrentTarget >= Targets.Length)
                        {
                            CurrentTarget = 0;
                            StageLightOn();               
                        }
                    }   
                    if (transform.position.x != Targets[CurrentTarget].transform.position.x)
                    {   
                        if (CameraSystemUI.activeSelf == true)
                        {                
                            if (Animatronic == 2 && Targets[CurrentTarget].GetComponent<DestinationPoint>().IsDoor)
                            {

                            }
                            else
                            {
                                CameraStatic.SetActive(true);
                                count = 0.5f;
                            }
                        }                     
                    }  
                }   
                var CoolDownTime = UnityEngine.Random.Range(MinCoolDownTime, MaxCoolDownTime);
                if (CoolDownTimer > 3)
                {
                    CoolDownTimer += CoolDownTime;
                }
                else
                {
                    CoolDownTimer = CoolDownTime; 
                }            
            }
            else
            {
                CoolDownTimer -= Time.deltaTime;
                if (closecams)
                {
                    closecamstime -= Time.deltaTime;
                }
            }   
            
            if (count <= 0)
            {
                CameraStatic.SetActive(false);
            }

            if (CameraSystemUI.activeSelf == false)
            {
                CameraStatic.SetActive(false);
            }
            count -= Time.deltaTime;
            transform.position = Targets[CurrentTarget].transform.position;        
            
        }
        if (CurrentTarget == Targets.Length - 1 && ShiftTimer.DeathScreen.activeSelf == false)    
        {
            PosX = Targets[CurrentTarget].transform.position.x + UnityEngine.Random.Range(-0.5f, 0.5f);
            Vector3 temp = new Vector3(PosX, Targets[CurrentTarget].transform.position.y, Targets[CurrentTarget].transform.position.z);
            transform.position = temp;
        }
    }

    public void ChangeAggByHour(int hour)
    {
        if(HoursChanged != hour)
        {
            if (ThresholdToPass <= AggressionByHour[hour])
            {
                ThresholdToPass = AggressionByHour[hour];  
            }            
            ThresholdToPass += UnityEngine.Random.Range(MinAggAdd, MaxAggAdd);
            HoursChanged += 1;
        }        
    }

    private void StageLightOn()
    {
        StageLight.SetActive(true);
    }
    private void StageLightOff()
    {
        StageLight.SetActive(false);
    }

    private void jumpscare()
    {
        closecams = true;
        IsJumpscare = true;
        JumpScareSound.Play();
    }
}
