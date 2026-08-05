using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
        [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;

    public bool TakeDamage()
{
   if (isInvincible) return false;
   
    isInvincible = true;
    StartCoroutine(InvincibilityTimer());
    StartCoroutine(FlashEffect());
    return true;
}

private IEnumerator InvincibilityTimer()
{
    yield return new WaitForSeconds(1.5f);
    isInvincible = false;
}

private IEnumerator FlashEffect()
{
    float elapsed = 0f;
    float flashInterval = 0.1f;

    while (elapsed < 1.5f)
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
        yield return new WaitForSeconds(flashInterval);
        elapsed += flashInterval;
    }

    spriteRenderer.enabled = true;   // make sure it ends visible, not stuck invisible
}


}
