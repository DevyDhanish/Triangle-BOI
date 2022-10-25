using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleanUP : MonoBehaviour
{
    public float cleanUpInterval;
    private float timer = 0f;

    void Update(){
        timer += Time.deltaTime;
        if(timer >= cleanUpInterval){
            try{
                Destroy(GameObject.FindGameObjectWithTag("bulletExplode"));
                timer = 0f;
            }
            catch{
                timer = 0f;
            }
        }
    }
}
