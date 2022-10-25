using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreupdate : MonoBehaviour
{
    public static scoreupdate sup;
    public int score = 0;
    private string scoreStr = "Score : {0}";
    public Text scorecounter;
    void Awake(){
        if(sup == null){
            sup = this;
        }
    }

    public void addscore(){
        score += 100;
        scorecounter.text = string.Format(scoreStr, score);
    }
}
