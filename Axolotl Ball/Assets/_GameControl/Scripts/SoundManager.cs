using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioSource sfxSource;
    AudioSource musicSource;
    AudioSource ambianceSouce;
    AudioSource sfxSource2;
    public AudioClip[] UIChangeSounds;
    public AudioClip[] music;
    public AudioClip[] ballBouncePlayerSfx;
    public AudioClip[] ballBounceWallSfx;

    public AudioClip[] croudSfx;
    public AudioClip scoreSound;
    private void Awake()
    {
        instance = this;
        AudioSource[] sources = GetComponents<AudioSource>();
        sfxSource = sources[0];
        musicSource = sources[1];
        ambianceSouce = sources[2];
        sfxSource2 = sources[3];
    }

    public void PlayUIChangeSound()
    {
        int randomIndex = Random.Range(0, UIChangeSounds.Length);
        // Assign the selected audio clip to the audio source
        sfxSource.clip = UIChangeSounds[randomIndex];

        // Play the audio clip
        sfxSource.Play();
    }

    public void PlayMusic(int songIndex, bool loop = false)
    {
        musicSource.clip = music[songIndex];
        musicSource.loop = loop;
        // Play the audio clip
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayPlayerHitBall()
    {
        int randomIndex = Random.Range(0, ballBouncePlayerSfx.Length);
        // Assign the selected audio clip to the audio source
        sfxSource.clip =  ballBouncePlayerSfx[randomIndex];
        // Play the audio clip
        sfxSource.Play();
    }

    public void PlayBallHitWall()
    {
        int randomIndex = Random.Range(0, ballBounceWallSfx.Length);
        // Assign the selected audio clip to the audio source
        sfxSource2.clip = ballBounceWallSfx[randomIndex];
        // Play the audio clip
        sfxSource2.Play();
    }

    public void PlayScoreSound()
    {
        sfxSource2.clip = scoreSound;
        // Play the audio clip
        sfxSource2.Play();
    }
}
