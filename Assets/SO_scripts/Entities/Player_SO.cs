using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom SO/player")]
public class Player_SO : ScriptableObject
{
    [Header("Logic")]
    public int _health;

    [Header("Movement")]
    public float _move_speed;
    public float _max_velocity;
}
