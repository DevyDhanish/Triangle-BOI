using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoil : MonoBehaviour
{
    private Rigidbody2D _player_rigidbody;

    void Start(){
        _player_rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void addrecoil(Transform _gunpoint, float _recoil_force){
        _player_rigidbody.AddForce(-_gunpoint.up * _recoil_force, ForceMode2D.Impulse);
    }
}
