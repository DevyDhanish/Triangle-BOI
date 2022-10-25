using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager instance;
    private AudioSource mt;

    void Start(){
        instance = this;
        mt = gameObject.GetComponent<AudioSource>();
    }
    
    public void startMusic(){
        if(!mt.isPlaying){
            mt.Play();
        }
    }
    public void stopMusic(){
        if(mt.isPlaying){
            mt.Stop();
        }
    }
}
