using UnityEngine;
using TMPro;
using NUnit.Framework.Internal.Execution;

public class UIhandler : MonoBehaviour
{
    [SerializeField] private Animator[] heartAnimators;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TMP_Text bestScoreText;
    //[SerializeField] private TextMeshProUGUI bestScore;


    int currentHearts =3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateHearts()
    {
        Debug.Log("UI lost heart");
        currentHearts--;

        heartAnimators[currentHearts].SetTrigger("LoseHeart");
    }

    public void UpdateScore(int Score)
    {
        scoreText.text = "Score: "+ Score ;
    }


    public void UpdateBestScore(int bestScore)

    {

        bestScoreText.text = bestScore.ToString();

    }

    public void HideHeart()
    {
        gameObject.SetActive(false);
    }
}
