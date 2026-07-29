using UnityEngine;

public class MonsterVisuals : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject angryIcon;
    [SerializeField] private GameObject calmingIcon;

    public void UpdateStateVisuals(AstarAI.MonsterState state)
    {
        // Hide icons by default
        if (angryIcon) angryIcon.SetActive(false);
        if (calmingIcon) calmingIcon.SetActive(false);

        switch (state)
        {
            case AstarAI.MonsterState.Waiting:
                if (spriteRenderer) spriteRenderer.color = Color.white;
                break;

            case AstarAI.MonsterState.Angry:
                if (spriteRenderer) spriteRenderer.color = Color.red;
                if (angryIcon) angryIcon.SetActive(true);
                break;

            case AstarAI.MonsterState.Chasing:
                if (spriteRenderer) spriteRenderer.color = new Color(1f, 0.5f, 0f); // Orange
                break;

            case AstarAI.MonsterState.Calming:
                if (spriteRenderer) spriteRenderer.color = Color.blue;
                if (calmingIcon) calmingIcon.SetActive(true);
                break;

            case AstarAI.MonsterState.Exiting:
                if (spriteRenderer) spriteRenderer.color = Color.green;
                break;
        }
    }
}

