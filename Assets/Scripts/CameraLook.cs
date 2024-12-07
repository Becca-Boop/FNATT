using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float CameraSensitivity = 100f;
    [SerializeField] private float MinLookDist;
    [SerializeField] private float MaxLookDist;
    [SerializeField] private float camlookDistance;
    [SerializeField] private AnimatronicSystem[] AnimatronicSystem;
    [SerializeField] private bool nomove;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camlookDistance = transform.localRotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < AnimatronicSystem.Length; i++)
        {
            if (AnimatronicSystem[i].IsJumpscare)
            {
                nomove = true;
            }
        }
        camlookDistance = Mathf.Clamp(camlookDistance + Input.GetAxis("Mouse X") * CameraSensitivity, MinLookDist, MaxLookDist);
        if(!nomove)
        {
            transform.localRotation = Quaternion.Euler(0f, camlookDistance, 0f);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            // trynig to get camera to stop moving when jumpscared
        }
        
    }
}
