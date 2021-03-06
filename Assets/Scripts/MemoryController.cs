using UnityEngine;

namespace CubeSurferClone
{
    public class MemoryController : SingletonMonoBehaviour<MemoryController>
    {
        [SerializeField] string lastOpenedLevelKey;
        [SerializeField] string diamondsKey;
        [SerializeField] string currentLevelKey;

        public int currentLevel
        {
            get => PlayerPrefs.GetInt(currentLevelKey, 0);
            set => PlayerPrefs.SetInt(currentLevelKey, value);
        }

        public int lastOpenedLevel
        {
            get => PlayerPrefs.GetInt(lastOpenedLevelKey, 0);
            set => PlayerPrefs.SetInt(lastOpenedLevelKey, Mathf.Max(value, lastOpenedLevel));
        }

        public int diamonds
        {
            get => PlayerPrefs.GetInt(diamondsKey, 0);
            set => PlayerPrefs.SetInt(diamondsKey, value);
        }

        public void SetCurrentLevel(int chosenLevel) => currentLevel = chosenLevel;
        public void UpdateDiamonds() => diamonds += ScoreController.Instance.currentScoreMultiplied;
        public void WinLevel() => lastOpenedLevel += (currentLevel == lastOpenedLevel ? 1 : 0);

    }   
}
