using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bullettime : MonoBehaviour
{
    public static bullettime bt;
    public float slowmoAmount;
    public float duration;
    public bool isInBulletTime = false;

    private Canvas sliderCanvas;
    private Slider slowmotimeSlider;
    private float timer;

    void Awake(){
        bt = this;
    }

    void Start(){
        sliderCanvas = GameObject.FindGameObjectWithTag("timerCan").GetComponent<Canvas>();
        slowmotimeSlider = GameObject.FindGameObjectWithTag("timerSlider").GetComponent<Slider>();
        sliderCanvas.enabled = false;
    }

    void Update(){
        if(isInBulletTime && gameLogic.gl.isGamerunning){
            timer += Time.unscaledDeltaTime;
            slowmotimeSlider.value -= Time.unscaledDeltaTime/(duration - 1);
            if(timer >= duration){
                Time.timeScale = 1f;
                Time.fixedDeltaTime = .02f;
                isInBulletTime = false;
                slowmotimeSlider.value = 0f;
                sliderCanvas.enabled = false;
                timer = 0f;
            }
        }
    }

    public void slowmo(){
        if(!isInBulletTime && gameLogic.gl.isGamerunning){
            Time.timeScale = slowmoAmount;
            Time.fixedDeltaTime = .02f * Time.timeScale;
            sliderCanvas.enabled = true;
            slowmotimeSlider.value = 1f;
            isInBulletTime = true;
        }
    }
}
