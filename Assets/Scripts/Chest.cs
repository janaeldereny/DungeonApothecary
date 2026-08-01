using System.Collections;
using UnityEngine;


public class Chest : MonoBehaviour
{
    //public ItemScriptableObject item;

    [SerializeField] private Animator animator;
    [SerializeField] public ItemScriptableObject item;
    private ItemScriptableObject originalItem;

    void Start()
    {
        originalItem = item;
    } 

    public void OpeningAnim()
    {
         animator.SetBool("isOpen", true);
    }
    public IEnumerator Respawn()
{
    yield return new WaitForSeconds(5f);

    item = originalItem;
    animator.SetBool("isOpen", false);
}

}
