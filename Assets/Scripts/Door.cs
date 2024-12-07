using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject Light;
    [SerializeField] private GameObject OfficeLight;
    [SerializeField] private Vector3 OpenPos;
    [SerializeField] private Vector3 ClosePos;
    [SerializeField] private float DoorSpeed;
    public bool IsOpen;
    public bool IsOn;
    [SerializeField] private PowerSystem Power;
    [SerializeField] private float flashlights;
    [SerializeField] private bool flashon;
    [SerializeField] private AudioSource LightNoise;
    [SerializeField] private AudioClip FlashNoise;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = OpenPos;
        IsOpen = true;

        ChangeLights();
        OfficeLight.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Power.Power > 0f)
        {
            if (IsOn && flashlights <= 0f)
            {
                flashlights = UnityEngine.Random.Range(0, 0.1f);
                if (Light.activeSelf == true)
                {
                    Light.SetActive(false);                    
                }
                else
                {
                    Light.SetActive(true);
                    LightNoise.PlayOneShot(FlashNoise, (flashlights));
                }                
            }
            flashlights -= Time.deltaTime;
            if (IsOpen)
            {
                if(transform.position != OpenPos)
                {
                    if (Vector3.Distance(transform.position, OpenPos) <= 0.5f)
                    {
                    transform.position = OpenPos;
                    }
                    else
                    {
                        transform.position = Vector3.Lerp(transform.position, OpenPos, DoorSpeed * Time.deltaTime);
                    }
                }            
            }
            else
            {
                if(transform.position != ClosePos)
                {
                    if (Vector3.Distance(transform.position, ClosePos) <= 0.5f)
                    {
                    transform.position = ClosePos;
                    }
                    else
                    {
                        transform.position = Vector3.Lerp(transform.position, ClosePos, DoorSpeed * Time.deltaTime);
                    }
                }         
            }
        } 
        else
        {
            if(transform.position != OpenPos)
            {
                if (Vector3.Distance(transform.position, OpenPos) <= 0.5f)
                {
                transform.position = OpenPos;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, OpenPos, DoorSpeed * Time.deltaTime);
                }
            }
            IsOn = true;
            ChangeLights();
            OfficeLight.SetActive(false);
        }
    }

    public void ChangeLights()
    {
        IsOn = !IsOn;

        if (IsOn)
        {
            Light.SetActive(true);
            //LightNoise.Play();
            Power.SystemsOn += 1;
        }
        else
        {
            Light.SetActive(false);
            //LightNoise.Stop();
            Power.SystemsOn -= 1;
        }
    }
}
