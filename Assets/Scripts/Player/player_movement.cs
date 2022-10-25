using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField] private Player_SO _player_status;
    [SerializeField] private Rigidbody2D _player_rigidbody;
    private Vector2 _move_direction;
    private float x, y;
    private Vector2 _ori_vel;
    private Vector2 _capped_vel;
    private Vector3 _mouse_pos;
    private Vector3 _direction;
    private float _angle;
    public bool _player_is_moving = false;

    public void Update(){
        if(gameLogic.gl.isGamerunning){
            lookAtMouse();
            if(_player_is_moving == false && zoom_out_and_in.instance._has_zoomed == true){
                zoom_out_and_in.instance.zoomin();
            } else {
                zoom_out_and_in.instance.zoomout();
            }
        }
    }

    public void FixedUpdate(){
        if(gameLogic.gl.isGamerunning && gameLogic.gl.gamePaused == false){
            movePlayer();
        }
    }

    private void getInputs(){
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        _move_direction = new Vector2(x, y);
        
        if(_move_direction == Vector2.zero){
            _player_is_moving = false;
        } else{
            _player_is_moving = true;
        }
    }

    private void movePlayer(){
        getInputs();
        try{
            if(!bullettime.bt.isInBulletTime){
            _player_rigidbody.AddForce(_move_direction * (_player_status._move_speed * 1000f) * Time.deltaTime);
            }
            else{
                transform.Translate(_move_direction * _player_status._move_speed * Time.unscaledDeltaTime, Space.World);
            }
        } catch {
            _player_rigidbody.AddForce(_move_direction * (_player_status._move_speed * 1000f) * Time.deltaTime);
        }
        
        speedControl();
    }

    private void speedControl(){
        _ori_vel = new Vector2(_player_rigidbody.velocity.x, _player_rigidbody.velocity.y);
        if(_ori_vel.magnitude > _player_status._max_velocity){
            _capped_vel = _ori_vel.normalized * _player_status._max_velocity;
            _player_rigidbody.velocity = new Vector2(_capped_vel.x, _capped_vel.y);
        }
    }

    private void lookAtMouse(){
        _mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = (_mouse_pos - transform.position).normalized;
        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _angle - 90f);
    }
}
