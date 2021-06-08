namespace CubeSurferClone
{
    public class ScoreController : SingletonMonoBehaviour<ScoreController>, IReset
    {
        public int currentScore { get; private set; }
        public int multiplier { get; private set; }

        public int currentScoreMultiplied { get => currentScore * multiplier; }

        public void Reset()
        {
            currentScore = 0;
            multiplier = 1;
        }

        public void AddScore() => ++currentScore;
        public void SetMultiplier(int value) => multiplier = value;
    }
}
