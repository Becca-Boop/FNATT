using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    [SerializeField] private AudioSource AmbientNoise;    
    [SerializeField] private ShiftTimer ShiftTimer;
    [SerializeField] private PowerSystem Power;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AmbientNoise.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShiftTimer.Dead || ShiftTimer.Won || Power.Power <= 0f)   
        {
            AmbientNoise.Stop();
        }        
    }
}
