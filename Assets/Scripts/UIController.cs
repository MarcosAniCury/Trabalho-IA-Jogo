using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //Public vars
    public Slider LifePlayerSlider;
    public GameObject GameOverPanel;
    public Text TimeSurviveEndGame;
    public Text TimeSurviveMaxEndGame;
    public Text KilledCount;

    //Private vars
    float bestTimeSurvive;
    int zombiesKilled;

    //Components
    PlayerController playerController;

    void Start()
    {
        playerController = GameObject.
            FindWithTag(Constants.TAG_PLAYER).
            GetComponent<PlayerController>();

        LifePlayerSlider.maxValue = playerController.myStatus.Life;
        LifePlayerSlider.value = playerController.myStatus.Life;

        bestTimeSurvive = PlayerPrefs.GetFloat(Constants.PLAYER_PREFS_MAX_TIME_SURVIVE);

        Time.timeScale = Constants.GAME_RESUME;
    }

    public void updatedLivePlayerSlider()
    {
        LifePlayerSlider.value = playerController.myStatus.Life;
    }

    public void GameOver()
    {
        Time.timeScale = Constants.GAME_PAUSE;
        GameOverPanel.SetActive(true);

        int min = (int)Time.timeSinceLevelLoad / 60;
        int seg = (int)Time.timeSinceLevelLoad % 60;
        TimeSurviveEndGame.text = "Sobreviveu  por  "+min+"min  e  "+seg+"s";

        MaxTimeSurvive(min, seg);
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AddOneZombieDead()
    {
        zombiesKilled++;
        KilledCount.text = string.Format("{0}", zombiesKilled);
    }

    void MaxTimeSurvive(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > bestTimeSurvive) {
            PlayerPrefs.SetFloat(Constants.PLAYER_PREFS_MAX_TIME_SURVIVE, Time.timeSinceLevelLoad);
            TimeSurviveMaxEndGame.text = "Tempo  maximo  "+min+"m  e  "+seg+"s";
            TimeSurviveEndGame.text = "MELHOR  TEMPO  ATINGIDO";
            TimeSurviveEndGame.color = Color.blue;
        } else {
            int bestMin = (int)bestTimeSurvive / 60;
            int bestSeg = (int)bestTimeSurvive % 60;
            TimeSurviveMaxEndGame.text = "Tempo  maximo  "+bestMin+"m  e  "+bestSeg+"s";
        }
    }
}
