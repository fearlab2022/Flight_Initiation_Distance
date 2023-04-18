using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [Tooltip("Adjusts the number of trials per set - recommend multiples of 10")]
    public int nTrialsPerSet = 10;
    //private Rigidbody3D body;
    //private bool score_increasing = true;
    private int score = 0;
    private int trialScore = 0;

    private TextMeshPro TMP;
    private const int MAX_SCORE = 100;
    private const int MAX_DISTANT = 80;
    private int trialCount = 0;

    /**
    reset trial score, state, increasing trail count and changes color and distribution of predators if necessary
    */
    public void reset()
    {
        Predator predator = GameObject.FindWithTag("Predator").GetComponent<Predator>();
        trialScore = 0;
        //score_increasing = true;
        if (predator.shuffleColorsAfterNTrials)
        {
            //print("Trial Count: " + trialCount + " vs nTrialsPerSet" + nTrialsPerSet);
            if (trialCount == nTrialsPerSet) predator.SetColorShuffleActive(true);
        }
        if (trialCount % nTrialsPerSet == 0 && trialCount > 0)
        {
            predator.shuffle_color();
            if (trialCount >= (2 * nTrialsPerSet))
            {
                predator.change_distribution_type();
            }
        }
        ++trialCount;

        //print("Trial Count: " + trialCount);
        TMP.text = $"Trial: {trialCount}\nScore: {trialScore}\nTotal: {score}";
    }

    /**
    clean trial score when caught
    */
    public void caught()
    {
        trialScore = 0;
    }

    /**
    add trial score to total score when escaped
    */
    public void escaped()
    {
        TMP.text = $"Trial: {trialCount}\nScore: {trialScore}\nTotal: {score}";
        score += trialScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        TMP = GetComponent<TextMeshPro>();
        reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            update_score();
        }
    }
    public int GetTrialCount()
    {
        return trialCount;
    }

    void update_score()
    {
        Vector2 playerPosition = GameObject.FindWithTag("Player").transform.position;

        Predator predator = GameObject.FindWithTag("Predator").GetComponent<Predator>();
        double attackingDistant = predator.get_attacking_distance();
        Vector2 predatorPosition = GameObject.FindWithTag("Predator").transform.position;

        trialScore = (int)(5 * (MAX_DISTANT - Vector2.Distance(playerPosition, predatorPosition)) /
        (MAX_DISTANT - attackingDistant)) + 1;
        //trialScore = (int)(100 * exp(1 - (MAX_DISTANT - attackingDistant) / 
        //(MAX_DISTANT - Vector2.Distance(playerPosition, predatorPosition)))) + 1
    }
}
