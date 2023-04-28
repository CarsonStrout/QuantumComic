using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private RewindTime rewindTime;
    [SerializeField] private CinemachineShake cinemachineShake;
    [SerializeField] private SpriteRenderer playerSprite;
    private Color tmp;
    [SerializeField] private AudioSource deathAudio;
    [SerializeField] private Button[] buttons;
    [SerializeField] private float speed;
    [SerializeField] private Transform respawnPos; // unique respawn point assigned in inspector for each "room"
    [SerializeField] private ParticleSystem deathParticle;
    private GameManager3 manager;

    private void Start()
    {
        tmp = playerSprite.color;
        manager = GameObject.Find("GameManager").GetComponent<GameManager3>();
    }

    private void Update()
    {
        tmp.a = Mathf.Lerp(tmp.a, 1, speed * Time.deltaTime);
        playerSprite.color = tmp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!rewindTime.isRewinding)
            {
                Instantiate(deathParticle, player.transform.position, deathParticle.transform.rotation);
                manager.playerDeath = true;
                RespawnPlayer();
            }
        }
    }

    void RespawnPlayer()
    {
        cinemachineShake.ShakeCamera(1f, 0.25f);
        deathAudio.Play();
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].buttonActivated = false;
        player.SetActive(false);
        tmp.a = 0;
        playerSprite.color = tmp;
        player.transform.position = respawnPos.position;
        player.SetActive(true);
    }
}
