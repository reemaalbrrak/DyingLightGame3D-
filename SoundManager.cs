using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource backgroundMusic;
    public AudioSource shootSound;
    public AudioSource footstepSound;
    public AudioSource zombieSpawnSound;
    public AudioSource winSound;
    public AudioSource loseSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        backgroundMusic.Play();
    }

    public void PlayShoot() => shootSound.Play();
    public void PlayFootstep() => footstepSound.Play();
    public void PlayZombieSpawn() => zombieSpawnSound.Play();
    public void PlayWin() => winSound.Play();
    public void PlayLose() => loseSound.Play();
}
