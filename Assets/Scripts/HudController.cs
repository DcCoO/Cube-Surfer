using TMPro;
using UnityEngine;

public class HudController : SingletonMonoBehaviour<HudController>
{
    [SerializeField] TMP_Text diamondCountText;
    public void UpdateHUD() => diamondCountText.text = $"{ScoreController.Instance.currentScore}";
}
