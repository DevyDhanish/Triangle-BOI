using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow_player : MonoBehaviour
{
    [SerializeField] private GameObject _target_to_follow;
    [SerializeField] private float _follow_speed;
    [SerializeField] private zoom_out_and_in _zoom_effect;

    private Vector3 _target_position;

    void LateUpdate(){
        if(_zoom_effect._has_zoomed && gameLogic.gl.isGamerunning){
            _target_position = new Vector3(_target_to_follow.transform.position.x, _target_to_follow.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, _target_position, _follow_speed * Time.deltaTime);
        }
    }   

}
