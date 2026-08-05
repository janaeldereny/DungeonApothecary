using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public event Action OnInventoryChanged;

   public ItemScriptableObject[] items = new ItemScriptableObject[2];

   public bool AddItem(ItemScriptableObject item)
   {
       for (int i = 0; i < items.Length; i++)
       {
           if (items[i] == null)
           {
               items[i] = item;
               OnInventoryChanged?.Invoke();
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

            OnInventoryChanged?.Invoke();

            return droppedItem;
        }

        else if (items[0] != null)
        {
            ItemScriptableObject droppedItem = items[0];
            items[0] = null;

            OnInventoryChanged?.Invoke();

            return droppedItem;
        }

       return null;
   }

   public void ConsumeAndCraft(ItemScriptableObject cure)
   {
        items[0]= cure;
        items[1] = null;
        OnInventoryChanged?.Invoke();
      
   }


}
