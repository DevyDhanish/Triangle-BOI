using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bullet_explode;
    private ParticleSystem _bullet_explode_PS;
    private Rigidbody2D _bullet_rigidbody;
    private Transform _Gun;
    private Gun_SO _current_gun_stat;
    private Transform target;

    void Start(){
        if(gameLogic.gl.isGamerunning){
            target = GameObject.FindGameObjectWithTag("player").transform;
        }
    }

    void Update(){
        destroyWhenFar();
    }

    void OnCollisionEnter2D(Collision2D _col){
        if(_col.transform.tag == "crate"){
            _col.transform.GetComponent<crate_logic>().crateTakeDamage(_current_gun_stat._damage);
        }
        
        if(_col.transform.tag == "enemy"){
            _col.transform.GetComponent<enemy_take_damage>().enemyTakeDamage(_current_gun_stat._damage);
        }

        if(_col.transform.tag == "player"){
            _col.transform.GetComponent<player_take_damage>().playerTakeDamage(_current_gun_stat._damage);
        }

        if(_col.transform.tag == "barrel"){
            _col.gameObject.GetComponent<barrelexplode>().barrelTakeDamage(_current_gun_stat._damage);
        }
        
        particle_spawner.ps.spawnParticle(this.transform, _bullet_explode);
        Destroy(gameObject);
    }

    public void addForce(Transform _direction, int _bullet_force, GameObject _gun){
        _Gun = _gun.transform;
        getCurrentWeaponSO();
        _bullet_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _bullet_rigidbody.AddForce(_direction.up * _bullet_force, ForceMode2D.Impulse);
    }

    private void getCurrentWeaponSO(){
        try{
            _current_gun_stat = _Gun.GetComponent<shooting>()._gun_stat;
        }
        catch {
            _current_gun_stat = _Gun.GetComponent<enemy_shooting>()._gun_stat;
        }
    }

    private void destroyWhenFar(){
        if(gameLogic.gl.isGamerunning){
            if(Vector3.Distance(transform.position, target.position) > 50f){
                Destroy(gameObject);
            }
        }
    }
}
