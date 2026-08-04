using UnityEngine;

public class FootStepSound : MonoBehaviour
{
     public AudioSource audioSource;
    public AudioClip[] footstepSounds ;
   


    private int lastIndex = -1;
    public void PlayFootstep()
    {
        if (footstepSounds.Length == 0) return;
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, footstepSounds.Length);
        }
        while (footstepSounds.Length > 1 && randomIndex == lastIndex);
        lastIndex = randomIndex;
        audioSource.PlayOneShot(footstepSounds[randomIndex]);
    }
}
