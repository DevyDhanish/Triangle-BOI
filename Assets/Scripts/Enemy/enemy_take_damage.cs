using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy_take_damage : MonoBehaviour
{
    [SerializeField] private int _enemy_health;
    [SerializeField] private ParticleSystem _e_death_ps;
    [SerializeField] private AudioSource _enemy_explode_sound;
    [SerializeField] private Slider enemyHealthSlider;
    [SerializeField] private SpriteRenderer enemysr;
    [SerializeField] private PolygonCollider2D enemyb2d;

    void Awake(){
        enemyHealthSlider.value = _enemy_health;
    }


    public void enemyTakeDamage(int damage){
        _enemy_health -= damage;
        if(_enemy_health <= 0){
            if(!_enemy_explode_sound.isPlaying){
                _enemy_explode_sound.Play();
            }
            enemyb2d.enabled = false;
            enemysr.enabled = false;
            Invoke("destroyEnemy", _enemy_explode_sound.clip.length);
        } 
        else{
            enemyHealthSlider.value = _enemy_health;
        }
    }
    
    private void destroyEnemy(){
        particle_spawner.ps.spawnParticle(this.transform ,_e_death_ps);
        scoreupdate.sup.addscore();
        dropthingswhendead.instance.drop(transform);
        Destroy(gameObject);
    }
}
