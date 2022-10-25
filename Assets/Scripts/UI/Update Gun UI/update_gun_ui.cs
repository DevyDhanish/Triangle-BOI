using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class update_gun_ui : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weapon_ui = new List<GameObject>();
    private GameObject _current_weapon_UI;

    void Start(){
        updateGunUI(_weapon_ui[0].tag);
    }

    void Update(){
        disableUnarmedWeaponUI();
    }

    public void updateGunUI(string _gunTag){
        for(int i = 0; i < _weapon_ui.Count; i++){
            if(_weapon_ui[i].tag == _gunTag){
                _current_weapon_UI = _weapon_ui[i];
            }
        }
    }

    private void disableUnarmedWeaponUI(){
        for(int j = 0; j < _weapon_ui.Count; j++){
            if(_weapon_ui[j].gameObject != _current_weapon_UI){
                _weapon_ui[j].SetActive(false);
            } else {
                _weapon_ui[j].SetActive(true);
            }
        }
    }
}
