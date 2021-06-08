using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CubeSurferClone
{    
    public class UIController : MonoBehaviour
    {
        [Header("Menu References")]
        [SerializeField] TMP_Text diamondCountText;
        [SerializeField] Button[] levelButtons;
        [SerializeField] TMP_Text[] levelTexts;

        [Header("Win/Lose Shared References")]
        [SerializeField] Image background;
        [SerializeField] RectTransform homeButton;
        [SerializeField] TMP_Text endText;
        [SerializeField] RectTransform nextButton;
        [SerializeField] RectTransform retryButton;

        [Header("Win Screen References")]
        [SerializeField] string winText;
        [SerializeField] string finishText;
        [SerializeField] RectTransform winDiamond;
        [SerializeField] TMP_Text winDiamondCountText;

        [Header("Lose Screen References")]
        [SerializeField] string loseText;

        private void Start() => UpdateMenu();
        public void UpdateMenu()
        {
            diamondCountText.text = $"{MemoryController.Instance.diamonds}";
            int lastOpenedLevel = MemoryController.Instance.lastOpenedLevel;
            for (int i = 0, len = levelButtons.Length; i < len; ++i)
            {
                if (i <= lastOpenedLevel)
                {
                    levelButtons[i].interactable = true;
                    levelTexts[i].text = $"{i + 1}";
                }
                else
                {
                    levelButtons[i].interactable = false;
                    levelTexts[i].text = "-";
                }
            }
        }

        public void WinGame() => StartCoroutine(EndGameRoutine(true));
        public void LoseGame() => StartCoroutine(EndGameRoutine(false));
        IEnumerator EndGameRoutine(bool win)
        {
            bool isLastLevel = MemoryController.Instance.currentLevel == levelTexts.Length - 1;

            //Reset all
            background.color = Color.clear;
            endText.text = (win ? (isLastLevel ? finishText : winText) : loseText);
            endText.rectTransform.localScale = Vector3.zero;
            nextButton.localScale = retryButton.localScale = Vector3.zero;
            homeButton.localScale = Vector3.zero;
            winDiamond.localScale = Vector3.zero;
            winDiamondCountText.rectTransform.localScale = Vector3.zero;
            if (win) winDiamondCountText.text =
                    $"{ScoreController.Instance.currentScore} x {ScoreController.Instance.multiplier} = {ScoreController.Instance.currentScoreMultiplied}";

            Color darkClear = new Color(0, 0, 0, 0.8f);
            for (float t = 0; t < 1; t += Time.deltaTime * 1.2f)
            {
                background.color = Color.Lerp(Color.clear, darkClear, t);
                yield return null;
            }
            background.color = darkClear;

            for (float t = 0; t < 1; t += Time.deltaTime * 1.6f)
            {
                endText.rectTransform.localScale = t * Vector3.one;
                yield return null;
            }
            endText.rectTransform.localScale = Vector3.one;

            RectTransform buttonRt = win ? nextButton : retryButton;
            for (float t = 0; t < 1; t += Time.deltaTime * 2f)
            {
                if (!win || (win && !isLastLevel)) buttonRt.localScale = t * Vector3.one;
                homeButton.localScale = t * Vector3.one;
                if (win)
                {
                    winDiamond.localScale = t * Vector3.one;
                    winDiamondCountText.rectTransform.localScale = t * Vector3.one;
                }
                yield return null;
            }
            if (!win || (win && !isLastLevel)) buttonRt.localScale = Vector3.one;
            homeButton.localScale = Vector3.one;
        }
    }
}

