using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Door Door;
    [SerializeField] private PowerSystem Power;

    private void OnMouseDown()
    {
        Door.IsOpen = !Door.IsOpen;
        if (Power.Power > 0f)
        {
            Door.GetComponent<AudioSource>().Play();
        }
        else
        {
            // play can't open door sound
        }        
        if (Door.IsOpen) 
        {
            Power.SystemsOn -= 1;
        }
        else
        {
            Power.SystemsOn += 1;
        }
    }
}
