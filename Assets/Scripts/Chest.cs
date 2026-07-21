using System.Collections;
using UnityEngine;


public class Chest : MonoBehaviour
{
    //public ItemScriptableObject item;


    [SerializeField] public ItemScriptableObject item;
    private ItemScriptableObject originalItem;

    void Start()
    {
        originalItem = item;
    } 

    public IEnumerator Respawn()
{
    yield return new WaitForSeconds(5f);

    item = originalItem;
}

}
