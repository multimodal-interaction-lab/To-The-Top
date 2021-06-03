using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BuildingBlock : MonoBehaviourPun
{

    public GameObject spawnMenuObject;
    public GameObject spawnPointObject;
    public int playerNum;   // which player spawned this block
    
    public SFXAsset despawnSound;
    public SFXAsset hitSound;
    public SFXAsset hardHitSound;

    AudioSource audioSrc;
    Rigidbody rigidbody;
    bool fresh = true;
    bool despawning = false;
    float timeFromDespawn;

    bool hitSoundPlayed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        Spawn();
    }

    private void FixedUpdate()
    {
        // Fail safe destruction if the despawn coroutine doesn't work
        if (despawning)
        {
            if (Time.time - timeFromDespawn > 0.3f)
            {
                if (PhotonNetwork.IsConnected == true)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    private void LateUpdate()
    {
        hitSoundPlayed = false;
    }
    public void BlockGrasped()
    {

        // Tell Spawn point to stop referencing this block once it has been grabbed and tell menu to replace the button
        if (fresh)
        {
            spawnPointObject.GetComponent<BlockGenerator>().spawnedBlock = null;
            spawnMenuObject.GetComponent<BlockSpawnMenu>().ReplaceButton();
        }
        fresh = false;
        gameObject.tag = "BlockInHand";
        rigidbody.isKinematic = false;
    }

    public void BlockReleased()
    {
        gameObject.tag = "BlockInPlay";
        rigidbody.isKinematic = false;
    }

    // Block will grow
    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    public IEnumerator SpawnCoroutine()
    {
        // Store the default scale as the final scale
        Vector3 finalScale = transform.localScale;
        transform.localScale = transform.localScale * 0.001f;

        // Increase the scale to the correct size
        while (Mathf.Abs(finalScale.x - transform.localScale.x) > 0.0001f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, finalScale, .2f);
            yield return new WaitForSeconds(.01f);
        }
    }
    //Normal Despawn plus the sound effect when block hits the ground
    public void BoundaryDespawn()
    {
        PlayDespawnSound();
        Despawn();
    }
    // Block will shrink and then disappear
    public void Despawn()
    {
        despawning = true;
        timeFromDespawn = Time.time;
        gameObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(DespawnCoroutine());
    }

    public IEnumerator DespawnCoroutine()
    {
        yield return null;
        while (transform.localScale.x > .01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), .2f);
            yield return new WaitForSeconds(.01f);
        }

        while (audioSrc.isPlaying)
        {
            yield return new WaitForSeconds(.2f);
        }

        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayDespawnSound()
    {
        AudioManager.Instance.PlaySFX(despawnSound, audioSrc);
    }
    
    public void PreventHitSoundThisFrame()
    {
        hitSoundPlayed = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        //Dont make sound if hand is is the one colliding
        if (!hitSoundPlayed && collision.gameObject.layer != 9)
        {
            //If colliding with block, make sure to only play one sound
            if(collision.gameObject.layer == 8)
            {
                collision.gameObject.GetComponent<BuildingBlock>().PreventHitSoundThisFrame();
            }

            if(collision.relativeVelocity.magnitude > 3.5f) {

                AudioManager.Instance.PlaySFX(hardHitSound, audioSrc);
                hitSoundPlayed = true;
            }
            else if (collision.relativeVelocity.magnitude > 1f)
            {
                AudioManager.Instance.PlaySFX(hitSound, audioSrc);
                hitSoundPlayed = true;
            }

        }
    }
}
