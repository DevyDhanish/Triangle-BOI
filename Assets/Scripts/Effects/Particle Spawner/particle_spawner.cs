using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_spawner : MonoBehaviour
{
    public static particle_spawner ps;
    private ParticleSystem _spawned_particle;

    void Awake(){
        if(ps == null){
            ps = this;
        }
    }

    public void spawnParticle(Transform _position, ParticleSystem _particle){
        _spawned_particle = Instantiate(_particle, _position.position, transform.rotation);
        //Destroy(_spawned_particle, _spawned_particle.main.duration);
    }
}
