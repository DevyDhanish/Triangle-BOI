using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom SO/Gun")]
public class Gun_SO : ScriptableObject
{
    public string _name;
    public float _fire_rate;
    public int _damage;
    public int _bullet_force;
    public float _recoil_force;
    public int _max_ammo;
}
