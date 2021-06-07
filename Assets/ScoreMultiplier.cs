using UnityEngine;

public class ScoreMultiplier : MonoBehaviour
{
    [SerializeField] int multiplier;
    [SerializeField] int heightToBreak;

    private void OnTriggerEnter(Collider other)
    {

        if(Player.Instance.numCollectibles >= heightToBreak + 2)
        {
            Player.Instance.BreakAtLevel(heightToBreak);
            //change multiplier
        }
        else
        {
            EventController.Instance.OnGameWin();
        }

    }
}