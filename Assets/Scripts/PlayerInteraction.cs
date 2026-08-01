using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InventoryUi inventoryUi;    
    [SerializeField] private Chest currentChest;
    [SerializeField] private CraftingTable table;
    [SerializeField] private Basket currentBasket;
    
    [SerializeField] private Inventory inventory;
    [SerializeField] private Enemy currentEnemy;
    [SerializeField] private CraftManager craftManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerHealth playerHealth;
     [SerializeField] private UIhandler uIhandler;

    //[SerializeField] public EnemySO enemyData;


    //SerializeField] private bool isFull = false;

    
     private void OnEnable()
    {
        GameInputManager.Instance.OnInteract += Interact_performed;
        GameInputManager.Instance.OnDrop += Drop_performed;
    }

    private void OnDisable()
    {
        GameInputManager.Instance.OnInteract -= Interact_performed;
        GameInputManager.Instance.OnDrop -= Drop_performed;
    }

    private void Interact_performed()
    {
        if (currentChest != null)
        {
             currentChest.OpeningAnim();
            
            if (inventory.AddItem(currentChest.item))
            {
                inventoryUi.Refresh(); 
                ItemScriptableObject added = currentChest.item;
                currentChest.item = null;
                StartCoroutine(currentChest.Respawn());

                Debug.Log (added + " item added");

            }
        
            else
            {
                Debug.Log ("inventory is full");
                //isFull = true;
            }
            
        }

        else if (table != null)
        {
               craftManager.startCrafting();
        }
    }

    private void Drop_performed()
    {
        if (currentBasket != null)
        {
            ItemScriptableObject droppedItem = inventory.DropItem();
            inventoryUi.Refresh(); 

            Debug.Log(droppedItem + " item dropped");
            //isFull = false;
        }
    }

    


    private void OnTriggerEnter2D(Collider2D other)
    {


       Chest chest = other.GetComponent<Chest>();
        if (chest != null)
        {
            currentChest = chest;

            //Debug.Log("Entered Chest");

        }

        Basket basket = other.GetComponent<Basket>();
        if (basket != null)
        {

            currentBasket = basket;

            //Debug.Log("Entered Basket");

        }

        CraftingTable craftingtable = other.GetComponent<CraftingTable>();
        if (craftingtable != null)
        {
             table = craftingtable;
             //Debug.Log("Entered table");
        }

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && ! gameManager.isGameover)
        {
            currentEnemy = enemy;
            Debug.Log("Touched an enemy");
{
        ItemScriptableObject heldCure = inventory.items[0];

            if (heldCure == enemy.enemyData.requiredCure)
            {
                gameManager.score++;
                uIhandler.UpdateScore(gameManager.score);

                Debug.Log("Score: " + gameManager.score + " | " + "Hearts: " + GameManager.Instance.hearts);
                Debug.Log("Monster Healed");
                currentEnemy.EnemyExits();
                inventory.items[0] = null;
                inventoryUi.Refresh();
            }
            else if (!currentEnemy.exiting)
            {
                playerHealth.TakeDamage();
                // gameManager.hearts--;
                uIhandler.UpdateScore(gameManager.score);
                GameManager.Instance.LoseHeart();

                //  uIhandler.UpdateHearts();
                Debug.Log("Score: " + gameManager.score + " | " + "Hearts: " + GameManager.Instance.hearts);

                
            }
    }

        }

    }
    

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.GetComponent<Chest>() == currentChest)
        {
            currentChest = null;
            //Debug.Log("Exit Chest");
        }

        else if (other.GetComponent<Basket>() == currentBasket)
        {
            currentBasket = null;

            //Debug.Log("Exit Basket");
        }

        else if (other.GetComponent<CraftingTable>() == table)
        {
            table = null;
             //Debug.Log("Exit table");
        }


        else if ( other.GetComponent<Enemy>() == currentEnemy)
        {
            currentEnemy = null;
              //Debug.Log("Exit enemy");
        }

    }


}
