using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropthingswhendead : MonoBehaviour
{
    public static dropthingswhendead instance;

    void Awake(){
        instance = this;
    }
    
    [SerializeField] private GameObject[] thingsToDrop;

    public void drop(Transform pos){
        Instantiate(thingsToDrop[Random.Range(0, thingsToDrop.Length)], pos.position, Quaternion.identity);
    }
}
