using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    
    [SerializeField] private Enemy currentEnemy;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private UIhandler uIhandler;
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventoryUi inventoryUi;  


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip touchedEnemySound;
    [SerializeField] private AudioClip correctCureSound;

    private void OnTriggerStay2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && ! GameManager.Instance.isGameover)
        {
            currentEnemy = enemy;
            HandleEnemyCollision(enemy);

        }
                
    }

    private void HandleEnemyCollision(Enemy enemy)
    {
        if (currentEnemy.currentState == EnemyStates.Exiting || currentEnemy.currentState == EnemyStates.Calming)
        {
            return;
        }

         ItemScriptableObject heldCure = inventory.items[0];
        //  inventory.GetHeldItem();

        if (heldCure == enemy.enemyData.requiredCure)
        {
            HealEnemy(enemy);
        }
        else
        {
            DamagePlayer();
        }
    }


    private void HealEnemy(Enemy enemy)
    {
        GameManager.Instance.AddScore(1);
        uIhandler.UpdateScore(GameManager.Instance.score);

        audioSource.PlayOneShot(correctCureSound);

        currentEnemy.EnterCalming();

        // inventory.RemoveHeldItem();
        inventory.items[0] = null;
        inventoryUi.Refresh();
    }

    private void DamagePlayer()
    {
        if (playerHealth.TakeDamage())
        {
            audioSource.PlayOneShot(touchedEnemySound);
            //uIhandler.UpdateScore(GameManager.Instance.score);
            GameManager.Instance.LoseHeart();
            
        }
    }
}
