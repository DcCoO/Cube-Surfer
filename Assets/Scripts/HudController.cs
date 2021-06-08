using TMPro;
using UnityEngine;

namespace CubeSurferClone
{
    public class HudController : SingletonMonoBehaviour<HudController>
    {
        [SerializeField] TMP_Text diamondCountText;
        public void UpdateHUD() => diamondCountText.text = $"{ScoreController.Instance.currentScore}";
    }
}
