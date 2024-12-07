using TMPro;
using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    [SerializeField] public int SystemsOn;
    [SerializeField] public float Power = 100;
    [SerializeField] public TextMeshProUGUI PowerText;
    [SerializeField] private AudioSource NoPowerNoise;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SystemsOn < 0)
        {
            SystemsOn = 0;
        }
        if (SystemsOn == 1)
        {
            Power -= 0.1f * Time.deltaTime;
        }
        else if (SystemsOn == 2)
        {
            Power -= 1f * Time.deltaTime;
        }
        else if (SystemsOn == 3)
        {
            Power -= 1.5f * Time.deltaTime;
        }
        else if (SystemsOn == 4)
        {
            Power -= 2f * Time.deltaTime;
        }
        else if (SystemsOn == 5)
        {
            Power -= 3f * Time.deltaTime;
        }

        var power = string.Format("{0:0}", Power);
        PowerText.text = $"{power}%";

        if (Power <= 0f)
        {
            NoPowerNoise.Play();
        }
    }
}
