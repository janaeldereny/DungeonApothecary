using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float fadeDuration = 1f;

    public void Die()
    {
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        GetComponent<PlayerMovement>().enabled = false;

        Color startColor = Color.white;
        Color endColor = Color.gray;

        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(startColor, endColor, t / fadeDuration);
            yield return null;
        }

        spriteRenderer.color = endColor;

        yield return new WaitForSeconds(0.5f);

       
        GameManager.Instance.GameOver();
    }
}

