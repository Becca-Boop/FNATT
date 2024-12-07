using UnityEngine;

public class LightControls : MonoBehaviour
{
    [SerializeField] private Door Door;

    private void OnMouseDown()
    {
        Door.ChangeLights();
    }
}
