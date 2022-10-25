using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        if(gameLogic.gl.isGamerunning){
            transform.position = target.position;  
        }
    }
}
