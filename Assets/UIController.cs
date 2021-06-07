using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [Header("Win/Lose Shared References")]
    [SerializeField] Image background;
    [SerializeField] RectTransform homeButton;
    [SerializeField] TMP_Text endText;
    [SerializeField] RectTransform nextButton;
    [SerializeField] RectTransform retryButton;

    [Header("Win Screen References")]
    [SerializeField] string winText;    

    [Header("Lose Screen References")]    
    [SerializeField] string loseText;


    public void WinGame() => StartCoroutine(EndGameRoutine(true));
    public void LoseGame() => StartCoroutine(EndGameRoutine(false));

    IEnumerator EndGameRoutine(bool win)
    {
        //Reset all
        background.color = Color.clear;
        endText.text = (win ? winText : loseText);
        endText.rectTransform.localScale = Vector3.zero;
        nextButton.localScale = retryButton.localScale = Vector3.zero;
        homeButton.localScale = Vector3.zero;


        Color darkClear = new Color(0, 0, 0, 0.8f);
        for (float t = 0; t < 1; t += Time.deltaTime * 1.2f)
        {
            background.color = Color.Lerp(Color.clear, darkClear, t);
            yield return null;
        }
        background.color = darkClear;
        
        for (float t = 0; t < 1; t += Time.deltaTime * 1.2f)
        {
            endText.rectTransform.localScale = t * Vector3.one;
            yield return null;
        }
        endText.rectTransform.localScale = Vector3.one;

        RectTransform buttonRt = win ? nextButton : retryButton;
        for (float t = 0; t < 1; t += Time.deltaTime * 1.2f)
        {
            buttonRt.localScale = t * Vector3.one;
            homeButton.localScale = t * Vector3.one;
            yield return null;
        }
        buttonRt.localScale = Vector3.one;
        homeButton.localScale = Vector3.one;
    }


}
