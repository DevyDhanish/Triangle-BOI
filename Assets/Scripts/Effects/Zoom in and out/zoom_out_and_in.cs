using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom_out_and_in : MonoBehaviour
{
    public static zoom_out_and_in instance;
    [SerializeField] private float _zoom_speed;
    [SerializeField] private float _final_oritho_size;
    [SerializeField] private float _zoomin_oritho_size;
    public bool _has_zoomed = false; 
    private Camera _camera;
    private bool _iszoomable;

    void Start(){
        instance = this;
        _camera = Camera.main;
        _iszoomable = true;
    }

    void Update(){
        checkzoom();
        if(_iszoomable){
            zoomout();
        }
        if(_camera.orthographicSize > _final_oritho_size - 1){
            _has_zoomed = true;
        }
    }

    public void zoomout(){
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _final_oritho_size, Time.deltaTime * _zoom_speed);
    }

    public void zoomin(){
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _zoomin_oritho_size, Time.deltaTime * _zoom_speed);
    }

    private void checkzoom(){
        if(_camera.orthographicSize >= (_final_oritho_size - 1f)){
            _iszoomable = false;
        } else {
            _iszoomable = true;
        }
    }
}
