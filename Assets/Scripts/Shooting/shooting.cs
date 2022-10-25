using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooting : MonoBehaviour
{

    public static shooting instance;
    public Gun_SO _gun_stat;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private recoil _recoil;
    [SerializeField] private AudioSource _shoot_audio;
    [SerializeField] private int ammo, magazine, reloadDuration, minAddAmmo, maxAddAmmo;
    [SerializeField] private Text ammoText;
    [SerializeField] private string ammoInfo;
    [SerializeField] private Canvas reloadingText;
    [SerializeField] private Canvas outOfAmmoText;

    private float _time_required_bs;
    private float _time_elapsed;
    private bool _can_shoot = true;
    private GameObject _ini_bullet;
    private int ammoC;
    private int magC;
    private bool isReloading = false;
    private bool ranOutOfAmmo = false;

    void Start(){
        instance = this;
        _time_required_bs = 60/_gun_stat._fire_rate;
        _shoot_audio.playOnAwake = false;
        _shoot_audio.loop = false;
        ammoC = ammo;
        magC = magazine;
        reloadingText.enabled = false;
        outOfAmmoText.enabled = false;
    }

    void Update(){
        magWarningUpdate();
        reloadingTextUpdate();

        ammoC = Mathf.Clamp(ammoC, 0, ammoC);
        magC = Mathf.Clamp(magC, 0, magC);

        ammoCal();
        ammoText.text = string.Format(ammoInfo, ammoC, magC);
        shotCalculations();

        if(Input.GetKey(KeyCode.Space) && _can_shoot && gameLogic.gl.isGamerunning && gameLogic.gl.gamePaused == false && !isReloading && !ranOutOfAmmo){
            triggerGun();
            _recoil.addrecoil(this.transform, (_gun_stat._recoil_force * 100f));
        }

        magCal();
    }   

    private void triggerGun(){
        _ini_bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        _ini_bullet.GetComponent<Bullet>().addForce(this.transform, _gun_stat._bullet_force, this.gameObject);
        ammoC -= 1;
        if(!_shoot_audio.isPlaying){
            _shoot_audio.Play();
        }
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

    private void ammoCal(){
        if(ammoC <= 0 && !isReloading && magC > 0){
            isReloading = true;
            Invoke("reload", reloadDuration);
        }
    }

    private void magCal(){
        if(magC <= 0 && ammoC <= 0){
            magC = 0;
            ammoC = 0;
            ranOutOfAmmo = true;
            isReloading = false;
        } else {
            ranOutOfAmmo = false;
        }
    }

    private void reload(){
        if(!ranOutOfAmmo){
            if(magC >= ammo){
                ammoC += ammo;
            }else{ammoC += magC;}
            magC -= ammoC;
            isReloading = false;
        }
    }

    public void addAmmo(){
        magC += Random.Range(minAddAmmo, maxAddAmmo);
    }

    private void reloadingTextUpdate(){
        if(isReloading){
            reloadingText.enabled = true;
        }
        else{
            reloadingText.enabled = false;
        }
    }

    private void magWarningUpdate(){
        if(ranOutOfAmmo){
            outOfAmmoText.enabled = true;
        }
        else {
            outOfAmmoText.enabled = false;
        }
    }
}
