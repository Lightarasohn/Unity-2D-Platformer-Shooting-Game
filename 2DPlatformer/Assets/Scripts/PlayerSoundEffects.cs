using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceEffects;
    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private AudioClip gameMusic;
    private bool isPlayingDashSound = false;
    private System.Random indexGenerator;
    private PlayerMovement playerMovement;
    void Start()
    {
        indexGenerator = new System.Random();
        playerMovement =  transform.GetComponent<PlayerMovement>();
        gameMusic = Resources.Load<AudioClip>("SoundEffects/GameAndMenuSounds/GameMusic");
    }

    void Update()
    {
        if (!audioSourceMusic.isPlaying)
        {
            playMusic();
        }
        if (!transform.GetComponent<PlayerMovement>().isDashing && !audioSourceEffects.isPlaying && playerMovement.isGrounded() && Mathf.Round(Mathf.Abs(transform.GetComponent<Rigidbody2D>().velocity.x))!= 0)
        {
            playWalkingSoundEffect();
        }
    }
    private void playWalkingSoundEffect()
    {
        soundEffect = Resources.Load<AudioClip>("SoundEffects/PlayerSoundEffects/PlayerWalking/PlayerWalking" + indexGenerator.Next(1, 3));
        audioSourceEffects.clip = soundEffect;
        audioSourceEffects.Play();
    }
    private void playMusic()
    {
        audioSourceMusic.clip = gameMusic;
        audioSourceMusic.Play();
    }
    public void playDashSoundEffect()
    {
        soundEffect = Resources.Load<AudioClip>("SoundEffects/PlayerSoundEffects/DashSound");
        audioSourceEffects.clip = soundEffect;
        audioSourceEffects.Play();
        isPlayingDashSound = false;
    }
    public void playJumpSoundEffect()
    {
        soundEffect = Resources.Load<AudioClip>("SoundEffects/PlayerSoundEffects/Jump");
        audioSourceEffects.clip = soundEffect;
        audioSourceEffects.Play();
    }
}
