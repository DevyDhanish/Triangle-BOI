using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameLogic : MonoBehaviour
{
    public static gameLogic gl;

    public GameObject visiableOnStart;
    public GameObject disabledOnStart;
    public GameObject whileGameisPaused;
    public GameObject whenplayerdie;
    public Canvas bulletTime;
    public Text highscore;
    public GameObject enemyPb;
    public GameObject crate;
    public GameObject barrel;
    public float barrelspawninterval;
    public float objectSpawnInterval;
    public Transform spawnAround;
    public int spawnOffset;
    public float spawnIntervel;
    public bool isGamerunning = false;
    public bool gamePaused = false;

    private float x;
    private float y;
    private Vector2 spawnPos;
    private float spawntimer;
    private float objspawntimer;
    private float barrelspawntimer;
    private GameObject spawnedEnemy;

    void Awake(){
        if(gl == null){
            gl = this;
        }
    }

    void Start(){
        disabledOnStart.SetActive(false);
        whileGameisPaused.SetActive(false);
        whenplayerdie.SetActive(false);
        bulletTime.enabled = false;
        audioManager.instance.startMusic();
    }

    void Update(){
        spawnCall();
        objspawnCall();
        barrelspawnCall();
        if(isGamerunning == false && Input.GetKeyDown(KeyCode.E) && zoom_out_and_in.instance._has_zoomed){
            isGamerunning = true;
            disabledOnStart.SetActive(true);
            visiableOnStart.SetActive(false);
            audioManager.instance.startMusic();
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && gamePaused == false && isGamerunning){
            gamePaused = true;
            whileGameisPaused.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void resume(){
        if(gamePaused == true && isGamerunning){
            gamePaused = false;
            whileGameisPaused.SetActive(false);
            Time.timeScale = 1f;
            audioManager.instance.startMusic();
        }
    }
    public void quit(){
        Application.Quit();
    }

    public void restart(){
        SceneManager.LoadScene("Level 1");
        audioManager.instance.startMusic();
    }

    public void gameover(){
        whenplayerdie.SetActive(true);
        highscore.text = string.Format("HighScore : {0}", scoreupdate.sup.score);
        disabledOnStart.SetActive(false);
        audioManager.instance.stopMusic();
    }

    private void spawnCall(){
        if(isGamerunning){
            spawntimer += Time.deltaTime;
            if(spawntimer >= spawnIntervel){
                spawnNewEnemy();
                spawntimer = 0f;
            }
        }
    }

    private void objspawnCall(){
        if(isGamerunning){
            objspawntimer += Time.deltaTime;
            if(objspawntimer >= objectSpawnInterval){
                spawnObjects();
                objspawntimer = 0f;
            }
        }
    }

    private void barrelspawnCall(){
        if(isGamerunning){
            barrelspawntimer += Time.deltaTime;
            if(barrelspawntimer >= barrelspawninterval){
                spawnBarrel();
                barrelspawntimer = 0f;
            }
        }
    }

    private void spawnNewEnemy(){
        pos();
        spawnPos = new Vector2(x,y);
        spawnedEnemy = Instantiate(enemyPb, spawnPos, Quaternion.identity);
    }

    private void spawnObjects(){
        pos();
        spawnPos = new Vector2(x,y);
        Instantiate(crate, spawnPos, Quaternion.identity);
    }

    private void spawnBarrel(){
        pos();
        spawnPos = new Vector2(x,y);
        Instantiate(barrel, spawnPos, Quaternion.identity);
    }

    private void pos(){
        x = Random.Range(0, (spawnAround.position.x + spawnOffset));
        y = Random.Range(0, (spawnAround.position.y + spawnOffset));
    }

}
