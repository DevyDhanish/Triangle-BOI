using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelexplode : MonoBehaviour
{
    public float blastRadius; 
    public int blastDamage;
    public int barrelHeath;
    public ParticleSystem explodeParticle;
    [SerializeField] private SpriteRenderer barrelsr;
    [SerializeField] private BoxCollider2D barrelb2d;
    [SerializeField] private AudioSource barrelexp;

    private Collider2D[] colliders;

    public void barrelTakeDamage(int damage){
        barrelHeath -= damage;
        if(barrelHeath <= 0){
            particle_spawner.ps.spawnParticle(transform, explodeParticle);
            explode();
        }
    }

    private void explode(){
        colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);

        foreach(Collider2D collider in colliders){
            if(collider.transform.tag == "enemy"){
                collider.gameObject.GetComponent<enemy_take_damage>().enemyTakeDamage(blastDamage);
            }

            if(collider.transform.tag == "player"){
                collider.gameObject.GetComponent<player_take_damage>().playerTakeDamage(blastDamage);
            }

            if(collider.transform.tag == "crate"){
                collider.transform.GetComponent<crate_logic>().crateTakeDamage(blastDamage);
            }
        }

        if(!barrelexp.isPlaying){
            barrelexp.Play();
        }

        barrelb2d.enabled = false;
        barrelsr.enabled = false;

        Invoke("destroybarrel", gameObject.GetComponent<AudioSource>().clip.length);
    }

    private void destroybarrel(){
        Destroy(gameObject);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
