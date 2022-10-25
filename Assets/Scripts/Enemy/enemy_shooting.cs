using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_shooting : MonoBehaviour
{
    public Gun_SO[] allWeapons;
    [SerializeField] private GameObject _bullet;

    private float _time_required_bs;
    private float _time_elapsed;
    private bool _can_shoot = true;
    private GameObject _ini_bullet;
    public Gun_SO _gun_stat;
    

    void Start(){
        _gun_stat = allWeapons[Random.Range(0, allWeapons.Length)];
        _time_required_bs = 60/_gun_stat._fire_rate;
    }

    void Update(){
        shotCalculations();
        if(_can_shoot && gameLogic.gl.isGamerunning  && gameLogic.gl.gamePaused == false){
            triggerGun();
        }
    }   

    private void triggerGun(){
        _ini_bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        _ini_bullet.GetComponent<Bullet>().addForce(this.transform, _gun_stat._bullet_force, this.gameObject);
        _can_shoot = false;
    }

    private void shotCalculations(){
        if(_can_shoot == false){
            _time_elapsed += Time.deltaTime;
            if(_time_elapsed >= _time_required_bs){
                _can_shoot = true;
                _time_elapsed = 0;
            }
        }
    }
}
