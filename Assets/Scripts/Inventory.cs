using UnityEngine;

public class Inventory : MonoBehaviour
{
     [SerializeField] private InventoryUi inventoryUi; 
   public ItemScriptableObject[] items = new ItemScriptableObject[2];
   public bool AddItem(ItemScriptableObject item)
   {
       for (int i = 0; i < items.Length; i++)
       {
           if (items[i] == null)
           {
               items[i] = item;
               return true;
           }
       }
       return false;
   }

   public ItemScriptableObject DropItem()
   {
      if (items[1] != null)
        {
            ItemScriptableObject droppedItem = items[1];
            items[1] = null;
            return droppedItem;
        }

        else if (items[0] != null)
        {
            ItemScriptableObject droppedItem = items[0];
            items[0] = null;
            return droppedItem;
        }

       return null;
   }

   public void ConsumeAndCraft(ItemScriptableObject cure)
   {
        items[0]= cure;
        items[1] = null;
        inventoryUi.Refresh();
      
   }


}
