using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // provides a globally accessible reference to the audio manager
    public static AudioManager Instance;

    // stores the audio source used for looping background music
    private AudioSource musicSource;
    // stores the audio source used for one shot sound effects
    private AudioSource sfxSource;

    // stores the background music played on the main menu
    private AudioClip menuMusic;
    // stores the background music played during gameplay
    private AudioClip gameplayMusic;
    // stores the sound played when the player collects a present
    private AudioClip presentSound;
    // stores the sound played when the player collects a boost
    private AudioClip boostSound;
    // stores the sound played when a boost ends
    private AudioClip boostEndSound;
    // stores the sound played when the player hits an obstacle
    private AudioClip crashSound;
    // stores the sound played when a menu button is pressed
    private AudioClip clickSound;
    // stores the sound played when the game is paused
    private AudioClip pauseSound;
    // stores the sound played when the game is resumed
    private AudioClip resumeSound;

    // keeps track of which music clip is currently playing so it is not restarted unnecessarily
    private AudioClip currentMusic;

    // initializes the audio manager and loads all of the audio clips
    private void Awake()
    {
        // checks whether an audio manager instance already exists
        if (Instance != null && Instance != this)
        {
            // removes duplicate audio manager objects
            Destroy(gameObject);
            return;
        }

        // sets this object as the shared audio manager instance
        Instance = this;
        // keeps the audio manager when switching between scenes
        DontDestroyOnLoad(gameObject);

        // creates the audio source used for background music
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        // allows the music to keep playing while the game is paused
        musicSource.ignoreListenerPause = true;
        musicSource.volume = 0.45f;

        // creates the audio source used for sound effects
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        // allows sound effects to play even when time scale is set to zero
        sfxSource.ignoreListenerPause = true;
        sfxSource.volume = 0.85f;

        // loads all of the audio clips from the resources folder
        menuMusic = Resources.Load<AudioClip>("Audio/bgm_menu");
        gameplayMusic = Resources.Load<AudioClip>("Audio/bgm_gameplay");
        presentSound = Resources.Load<AudioClip>("Audio/present");
        boostSound = Resources.Load<AudioClip>("Audio/boost");
        boostEndSound = Resources.Load<AudioClip>("Audio/boost_end");
        crashSound = Resources.Load<AudioClip>("Audio/crash");
        clickSound = Resources.Load<AudioClip>("Audio/click");
        pauseSound = Resources.Load<AudioClip>("Audio/pause");
        resumeSound = Resources.Load<AudioClip>("Audio/resume");

        // listens for scene changes so the music can switch between the menu and the game
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    // starts the music for the scene that is already open
    private void Start()
    {
        // plays the correct music for the scene that is currently open
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    // stops listening for scene changes if this audio manager is destroyed
    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= HandleSceneLoaded;
        }
    }

    // plays the correct music whenever a new scene is loaded
    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // restores the normal music volume after pause or game over
        musicSource.volume = 0.45f;
        PlayMusicForScene(scene.name);
    }

    // chooses the background music based on the name of the current scene
    private void PlayMusicForScene(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            PlayMusic(menuMusic);
        }
        else
        {
            PlayMusic(gameplayMusic);
        }
    }

    // starts looping the given music clip if it is not already playing
    private void PlayMusic(AudioClip clip)
    {
        // does nothing if the requested clip is missing
        if (clip == null)
        {
            return;
        }

        // keeps the same track playing if it is already the current music
        if (currentMusic == clip && musicSource.isPlaying)
        {
            musicSource.volume = 0.45f;
            return;
        }

        currentMusic = clip;
        musicSource.clip = clip;
        musicSource.volume = 0.45f;
        musicSource.Play();
    }

    // plays a one shot sound effect if the clip exists
    private void PlaySound(AudioClip clip)
    {
        if (clip == null || sfxSource == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip);
    }

    // plays the present collection sound
    public void PlayPresent()
    {
        PlaySound(presentSound);
    }

    // plays the boost collection sound
    public void PlayBoost()
    {
        PlaySound(boostSound);
    }

    // plays the sound used when a boost ends
    public void PlayBoostEnd()
    {
        PlaySound(boostEndSound);
    }

    // plays the crash sound and lowers the music when the game ends
    public void PlayCrash()
    {
        PlaySound(crashSound);
        musicSource.volume = 0.15f;
    }

    // plays the menu button click sound
    public void PlayClick()
    {
        PlaySound(clickSound);
    }

    // plays the pause sound and lowers the music
    public void PlayPause()
    {
        PlaySound(pauseSound);
        musicSource.volume = 0.15f;
    }

    // plays the resume sound and restores the music volume
    public void PlayResume()
    {
        PlaySound(resumeSound);
        musicSource.volume = 0.45f;
    }
}
