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
    


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip inventoryFullSound;
    [SerializeField] private AudioClip craftingSound;
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip touchedEnemySound;
    [SerializeField] private AudioClip correctCureSound;



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
             
            
            if (inventory.AddItem(currentChest.item))
            {
                currentChest.OpeningAnim();
                inventoryUi.Refresh(); 
                ItemScriptableObject added = currentChest.item;
                currentChest.item = null;
                StartCoroutine(currentChest.Respawn());

                Debug.Log (added + " item added");

                audioSource.PlayOneShot(pickupSound);

            }
        
            else
            {
                Debug.Log ("inventory is full");

                audioSource.PlayOneShot(inventoryFullSound);

                //isFull = true;
            }
            
        }

        else if (table != null)
        {
               craftManager.startCrafting();

               audioSource.PlayOneShot(craftingSound);
        }
    }

    private void Drop_performed()
    {
        if (currentBasket != null)
        {
            ItemScriptableObject droppedItem = inventory.DropItem();
            inventoryUi.Refresh(); 

            audioSource.PlayOneShot(dropSound);

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

    }
}