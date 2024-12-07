using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] public GameObject[] Cameras;
    [SerializeField] public int CurrentCam;
    [SerializeField] private KeyCode OpenCameras;
    [SerializeField] private bool CamerasOpen;
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private float CoolDownTimer;
    [SerializeField] private float CoolDownTime = 0.5f;
    [SerializeField] public GameObject CameraSystemUI;
    [SerializeField] private PowerSystem Power;
    [SerializeField] private AnimatronicSystem[] AnimatronicSystem;
    [SerializeField] private bool camsdown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].SetActive(false);
        }
        CameraSystemUI.SetActive(false);
        MainCamera.SetActive(true);

        Cameras[CurrentCam].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < AnimatronicSystem.Length; i++)
        {
            if (AnimatronicSystem[i].closecams && (AnimatronicSystem[i].closecamstime <= 0f || CameraSystemUI.activeSelf == false))
            {
                camsdown = true;
            }
        }

        if (Power.Power > 0f && !camsdown)
        {
            if (Input.GetKeyDown(OpenCameras))
            {
                CamerasOpen = !CamerasOpen;
                if (CamerasOpen)
                {
                    Power.SystemsOn += 1;
                }
                else
                {
                    Power.SystemsOn -= 1;
                }
                ShowCamera();
            }

            if (CoolDownTimer <= 0)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    Cameras[CurrentCam].SetActive(false);
                    CurrentCam += 1;
                    if (CurrentCam >= Cameras.Length)
                    {
                        CurrentCam = 0;
                    }
                    GoToCamera(CurrentCam);
                    CoolDownTimer = CoolDownTime;
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    CurrentCam -= 1;
                    if (CurrentCam < 0)
                    {
                        CurrentCam = Cameras.Length -1;
                    }
                    GoToCamera(CurrentCam);
                    CoolDownTimer = CoolDownTime;
                }
            }
            else
            {
                CoolDownTimer -= Time.deltaTime;
            }
        }
        else
        {
            CamerasOpen = false;
            ShowCamera();
        }
    }

    private void ShowCamera()
    {
        if (CamerasOpen)
        {
            Cameras[CurrentCam].SetActive(true);
            CameraSystemUI.SetActive(true);
            MainCamera.SetActive(false);            
        }
        else
        {
            Cameras[CurrentCam].SetActive(false);
            CameraSystemUI.SetActive(false);
            MainCamera.SetActive(true);            
        }
    }

    public void GoToCamera(int Progression)
    {
        Cameras[CurrentCam].SetActive(false);
        CurrentCam = Progression;
        ShowCamera();
    }
}
