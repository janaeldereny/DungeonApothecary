//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    public Image item1Image;
    public Image item2Image;

    public void Refresh()
    {
        item1Image.enabled = inventory.items[0] != null;
        item2Image.enabled = inventory.items[1] != null;

        if (inventory.items[0] != null)
            item1Image.sprite = inventory.items[0].itemIcon;
            Debug.Log(item1Image.sprite);

        if (inventory.items[1] != null)
            item2Image.sprite = inventory.items[1].itemIcon;
    }

    private void OnEnable()
    {
        inventory.OnInventoryChanged += Refresh;
    }

    private void OnDisable()
    {
        inventory.OnInventoryChanged -= Refresh;
    }

}
