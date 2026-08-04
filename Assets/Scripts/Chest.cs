using System.Collections;
using UnityEngine;


public class Chest : MonoBehaviour
{
    //public ItemScriptableObject item;

    [SerializeField] private Animator animator;
    [SerializeField] public ItemScriptableObject item;
    private ItemScriptableObject originalItem;
    public GameObject icon;

    public AudioSource audioSource;
    public AudioClip chestOpenSound;
    //public AudioClip chestCloseSound;
    public AudioClip chestRefilledSound;


    void Start()
    {
        originalItem = item;
        icon.SetActive(true);
    } 

    public void OpeningAnim()
    {
         animator.SetBool("isOpen", true);
         icon.SetActive(false);
         audioSource.PlayOneShot(chestOpenSound);
    }
    public IEnumerator Respawn()
{
    yield return new WaitForSeconds(5f);

    item = originalItem;
    icon.SetActive(true);
    animator.SetBool("isOpen", false);
    audioSource.PlayOneShot(chestRefilledSound);

}

}
