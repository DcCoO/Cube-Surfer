using UnityEngine;

namespace CubeSurferClone
{
    public class ScoreMultiplier : MonoBehaviour
    {
        [SerializeField] int multiplier;
        [SerializeField] int heightToBreak;

        private void OnTriggerEnter(Collider other)
        {

            if (Player.Instance.numCollectibles >= heightToBreak + 2)
            {
                Player.Instance.BreakAtLevel(heightToBreak);
                ScoreController.Instance.SetMultiplier(multiplier);
            }
            else
            {
                AudioController.Instance.PlayHit();
                EventController.Instance.OnGameWin();
            }

        }
    }
}
