using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class MenuScript : MonoBehaviour
{
    [SerializeField] private TextAsset textAssetNames;
    [SerializeField] private string [] lines;
    [SerializeField] public int night;
    [SerializeField] public int playnight;
    [SerializeField] private bool beatgame;
    [SerializeField] private bool beat6;
    [SerializeField] private bool beat7;
    [SerializeField] private GameObject Night6;
    [SerializeField] private GameObject CustomNight;
    [SerializeField] private GameObject Continue;
    [SerializeField] private GameObject Star1;
    [SerializeField] private GameObject Star2;
    [SerializeField] private GameObject Star3;
    [SerializeField] private string CurrentNight;
    [SerializeField] private TextMeshProUGUI ContinueNight;
    [SerializeField] private MainManager MainManager;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateNight();
    }

    void Awake()
    {
        UpdateNight();
    }


    public void PlayGame(int Cnight)
    {
        MainManager.Instance.night = Cnight;
        if (Cnight == 6)
        {
            MainManager.Instance.night7AI = 20;
        }
        SceneManager.LoadSceneAsync(1);
    }

    public void ContinueGame()
    {
        playnight = night-1;
        MainManager.Instance.night = playnight;
        SceneManager.LoadSceneAsync(1);
    }  

    private void UpdateNight()
    {
        Night6.SetActive(false);
        CustomNight.SetActive(false);
        Continue.SetActive(false);
        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);



        string path = Application.dataPath + "/Resources/data.txt";
        var contents = File.ReadAllLines(path);
        
        night = int.Parse(contents[0].Substring(8));
        beatgame = bool.Parse(contents[1].Substring(11));
        beat6 = bool.Parse(contents[2].Substring(8));
        beat7 = bool.Parse(contents[3].Substring(8));


        if (night > 1)
        {
            Continue.SetActive(true);
            CurrentNight = string.Format("Night {0}", night);
            ContinueNight.text = CurrentNight;   
        }
        if (beatgame)
        {
            Night6.SetActive(true);
            Star1.SetActive(true);
        }
        if (beat6)
        {
            CustomNight.SetActive(true);
            Star2.SetActive(true);
        }
        if (beat7)
        {
            Star3.SetActive(true);
        }
    }
}
