using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : SingletonMonoBehaviour<ScoreController>, IReset
{
    [SerializeField] int currentScore;
    [SerializeField] int multiplier;

    public void Reset()
    {
        currentScore = 0;
        multiplier = 1;
    }

    public void AddScore() => ++currentScore;
    public void SetMultiplier(int value) => multiplier = value;
}
