using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool isGameover;
    public int score=0;
    public int bestScore;
    public int hearts=3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hearts <= 0)
        {
            Debug.Log ("Game Over");
            isGameover=true;
            GameOver();
        }
    }

    void  GameOver()
    {

    }
}
