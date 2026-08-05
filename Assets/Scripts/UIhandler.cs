using UnityEngine;
using TMPro;

public class UIhandler : MonoBehaviour
{
    [SerializeField] private Animator[] heartAnimators;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int currentHearts =3;

    
    public void UpdateHearts()
    {
        Debug.Log("UI lost heart");
        currentHearts--;
        if (currentHearts >= 0)
        {
            heartAnimators[currentHearts].SetTrigger("LoseHeart");
        }
    }

    public void UpdateScore(int Score)
    {
        scoreText.text = "Score: "+ Score ;
    }

    public void HideHeart()
    {
        gameObject.SetActive(false);
    }

}
