using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "bullettimepu"){
            bullettime.bt.slowmo();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if(!collision.gameObject.GetComponent<AudioSource>().isPlaying){
                collision.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        if(collision.transform.tag == "ammo"){
            shooting.instance.addAmmo();
            StartCoroutine(destoryObj(collision.gameObject));
        }
        if(collision.transform.tag == "health"){
            player_take_damage.instance.resetHealth();
            StartCoroutine(destoryObj(collision.gameObject));
        }
    }

    IEnumerator destoryObj(GameObject Obj){
        try{
            Obj.GetComponent<SpriteRenderer>().enabled = false;
            Obj.GetComponent<BoxCollider2D>().enabled = false;
        } catch {
            // PASS
        }
        if(!Obj.GetComponent<AudioSource>().isPlaying){
            Obj.GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(Obj.GetComponent<AudioSource>().clip.length);
        Destroy(Obj);
    }
}
