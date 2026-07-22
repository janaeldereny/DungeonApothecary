using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InventoryUi inventoryUi;    
    private Chest currentChest;
    private Basket currentBasket;
    [SerializeField] private Inventory inventory;
    
     private void OnEnable()
    {
        GameInputManager.Instance.OnInteract += Interact_performed;
        GameInputManager.Instance.OnDrop += Drop_performed;
    }

    private void Interact_performed()
    {
        if (currentChest != null)
        {
            
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
            }
            
        }
    }

    private void Drop_performed()
    {
        if (currentBasket != null)
        {
            ItemScriptableObject droppedItem = inventory.DropItem();
            inventoryUi.Refresh(); 

            Debug.Log(droppedItem + " item dropped");
        }
    }

    private void OnDisable()
    {
        GameInputManager.Instance.OnInteract -= Interact_performed;
        GameInputManager.Instance.OnDrop -= Drop_performed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
       Chest chest = other.GetComponent<Chest>();

        if (chest != null)

        {

            currentChest = chest;

            Debug.Log("Entered Chest");

        }

        Basket basket = other.GetComponent<Basket>();

        if (basket != null)

        {

            currentBasket = basket;

            Debug.Log("Entered Basket");

        }

    }
    

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.GetComponent<Chest>() == currentChest)
        {
            
            currentChest = null;
            Debug.Log("Exit Chest");
        }
        else if (other.GetComponent<Basket>() == currentBasket)
        {
            
            currentBasket = null;

            Debug.Log("Exit Basket");
        }
    }


}
