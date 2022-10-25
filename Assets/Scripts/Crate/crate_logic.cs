using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crate_logic : MonoBehaviour
{
    [SerializeField] private crate_SO _crate_stat;
    [SerializeField] private AudioSource _crate_break_audioS;
    private float _crate_health;

    void Start(){
        _crate_health = _crate_stat._crate_health;
        _crate_break_audioS.playOnAwake = false;
        _crate_break_audioS.loop = false;
    }

    public void crateTakeDamage(int _damage){
        _crate_health -= _damage;
        if(_crate_health <= 0f){
            dropthingswhendead.instance.drop(transform);
            if(!_crate_break_audioS.isPlaying){
                _crate_break_audioS.Play();
            }
            Destroy(gameObject);
        }
    }
}
