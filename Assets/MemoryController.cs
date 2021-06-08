using UnityEngine;

public class MemoryController : SingletonMonoBehaviour<MemoryController>
{
    [SerializeField] string lastOpenedLevelKey;
    [SerializeField] string diamondsKey;
    [SerializeField] string currentLevelKey;

    public int currentLevel
    {
        get => PlayerPrefs.GetInt(currentLevelKey, 0);
        set => PlayerPrefs.SetInt(currentLevelKey, Mathf.Max(value, lastOpenedLevel));
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
    public void AddDiamonds(int diamonds) => this.diamonds += diamonds;    
    public void WinLevel(int levelNumber) => lastOpenedLevel += (levelNumber == lastOpenedLevel ? 1 : 0);
    
}
