using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pick_up_gun : MonoBehaviour
{
    [SerializeField] private inventory_manager _inventory;
    [SerializeField] private List<GameObject> _gun_to_add = new List<GameObject>();
    [SerializeField] private AudioSource _picked_up_gun_sound;
    [SerializeField] private Text gunInfo;
    [SerializeField] private string defaultResponceToPickUP;
    private string added = "{0} Added";
    private GameObject _gun_to_add_GO;

    void Start(){
        gunInfo.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D _col){
        for(int i = 0; i < _gun_to_add.Count; i++){
            if(_gun_to_add[i].tag == _col.gameObject.tag){
                _gun_to_add_GO = _gun_to_add[i];
                if(!_inventory._weapons.Contains(_gun_to_add_GO)){
                    _inventory._weapons.Add(_gun_to_add_GO);
                    StartCoroutine(gunInfoUI(string.Format(added, _gun_to_add_GO.name)));
                    if(!_picked_up_gun_sound.isPlaying){
                        _picked_up_gun_sound.Play();
                    }
                    Destroy(_col.gameObject);
                }
                else{
                    StartCoroutine(gunInfoUI(defaultResponceToPickUP));
                }
            }
        }
    }

    IEnumerator gunInfoUI(string info){
        gunInfo.enabled = true;
        gunInfo.text = info;
        yield return new WaitForSeconds(1f);
        gunInfo.enabled = false;
    }
}
