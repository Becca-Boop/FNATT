using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public int night;
    public int night7AI;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
