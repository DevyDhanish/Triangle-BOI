using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_take_damage : MonoBehaviour
{
    public static player_take_damage instance;

    [SerializeField] private Player_SO _player_stat;
    [SerializeField] private Text _health_text;
    [SerializeField] private ParticleSystem _p_death_partical_system;
    [SerializeField] private AudioSource _player_hurt_sound;
    [SerializeField] private AudioSource _player_death_sound;
    [SerializeField] private Image heartImg;
    [SerializeField] private PolygonCollider2D playerb2d;
    [SerializeField] private SpriteRenderer playersr;
    private int _health;

    void Start(){
        instance = this;
        _health = _player_stat._health;
        _health_text.text = _health.ToString();
    }

    void Update(){
        if(gameLogic.gl.isGamerunning){
            _health_text.text = _health.ToString();
        }
    }

    public void playerTakeDamage(int damage){
            _health -= damage;
            if(!_player_hurt_sound.isPlaying){
                _player_hurt_sound.Play();
            }
            if(_health <= 0){
                _health_text.text = "0";
                particle_spawner.ps.spawnParticle(this.transform, _p_death_partical_system);
                gameLogic.gl.isGamerunning = false;
                gameLogic.gl.gameover();
                if(!_player_death_sound.isPlaying){
                    _player_death_sound.Play();
                }
                playersr.enabled = false;
                playerb2d.enabled = false;
                Invoke("destroyPlayer", _player_death_sound.clip.length);
            }
            else {
                StartCoroutine(chanagecolor());
            }
    }

    private void destoryPlayer(){
        Destroy(gameObject);
    }

    IEnumerator chanagecolor(){
        var tempColor = heartImg.color;
        tempColor.a = 0;
        heartImg.color = tempColor;
        yield return new WaitForSeconds(0.2f);
        tempColor.a = 1;
        heartImg.color = tempColor;
    }

    public void resetHealth(){
        if(gameLogic.gl.isGamerunning){
            _health = _player_stat._health;
        }
    }
}
