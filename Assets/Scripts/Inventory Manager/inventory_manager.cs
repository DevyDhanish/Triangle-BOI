using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_manager : MonoBehaviour
{
    public int _current_weapon_index = 0;
    public List<GameObject> _weapons = new List<GameObject>();
    [SerializeField] private update_gun_ui _update_gun_ui;
    public GameObject _current_weapon;

    void Start(){
        _current_weapon = _weapons[_current_weapon_index];
        _update_gun_ui.updateGunUI(_weapons[_current_weapon_index].tag);
        disableUnarmedWeapon();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Q)){
            _current_weapon_index += 1;
            changeWeapon();
        }
    }

    private void changeWeapon(){
        if(_current_weapon_index >= _weapons.Count){
            _current_weapon_index = 0;
            _current_weapon = _weapons[_current_weapon_index];
            //_weapons[_current_weapon_index].SetActive(true);
        } else {
            //_weapons[_current_weapon_index].SetActive(true);
            _current_weapon = _weapons[_current_weapon_index];
        }
        disableUnarmedWeapon();
    }

    private void disableUnarmedWeapon(){
        for(int i = 0; i < _weapons.Count; i ++){
            if(_weapons[i] != _current_weapon){
                _weapons[i].SetActive(false);
            } else {
                _weapons[_current_weapon_index].SetActive(true);
                _update_gun_ui.updateGunUI(_weapons[_current_weapon_index].tag);
            }
        }
    }
}
