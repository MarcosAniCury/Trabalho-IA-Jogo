using UnityEngine;

public class BossGenerate : MonoBehaviour
{
    //Public vars
    public GameObject BossPrefab;
    
    //Private vars
    private float timeToNextGenerate = 0;

    private void Start()
    {
        timeToNextGenerate = Constants.BOSS_GENERATE_TIME_BETWEEN_GENERATE;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > timeToNextGenerate)
        {
            Instantiate(BossPrefab, transform.position, Quaternion.identity);
            timeToNextGenerate = Time.timeSinceLevelLoad + Constants.BOSS_GENERATE_TIME_BETWEEN_GENERATE;
        }
    }
}
