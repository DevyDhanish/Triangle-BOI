using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    [SerializeField] private float _enemy_move_Speed;
    [SerializeField] private float _enemy_max_velocity;
    [SerializeField] private Player_SO _player_stat;
    [SerializeField] private GameObject _gun;
    [SerializeField] private float _min_distance;

    private Transform _target_position;
    private Rigidbody2D _enemy_rigidbody;
    private Vector2 _ori_vel;
    private Vector2 _capped_vel;
    private float _angle;
    private Vector2 _distance_to_target;
    private Vector2 _direction_to_target;

    void Start(){
        _target_position = GameObject.FindGameObjectWithTag("player").transform;
        _enemy_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _gun.SetActive(true);
    }

    void Update(){
        if(gameLogic.gl.isGamerunning && gameLogic.gl.gamePaused == false){
            lookAtPlayer();
            if(Vector2.Distance(this.transform.position, _target_position.position) > _min_distance){
                moveEnemy();
            }
        }
    }

    private void moveEnemy(){
        _enemy_rigidbody.AddForce(_direction_to_target * _enemy_move_Speed * 100f * Time.deltaTime);
        speedControl();
    }

    private void speedControl(){
        _ori_vel = new Vector2(_enemy_rigidbody.velocity.x, _enemy_rigidbody.velocity.y);
        if(_ori_vel.magnitude > _enemy_max_velocity){
            _capped_vel = _ori_vel.normalized * _enemy_max_velocity;
            _enemy_rigidbody.velocity = new Vector2(_capped_vel.x, _capped_vel.y);
        }
    }

    private void lookAtPlayer(){
        _distance_to_target = _target_position.position - transform.position;
        _direction_to_target = _distance_to_target.normalized;
        _angle = Mathf.Atan2(_direction_to_target.y, _direction_to_target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _angle - 90f);
    }
}
