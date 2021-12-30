using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterData : ScriptableObject 
{
    [SerializeField] int _healthMax = 1;
    public int healthMax => _healthMax;  

    [SerializeField] int _shieldMax = 1;
    public int shieldMax => _shieldMax;

    [SerializeField] float _moveSpeed = 10;
    public float moveSpeed => _moveSpeed;

    [SerializeField] float _jumpForce = 10;
    public float jumpForce => _jumpForce;

    [SerializeField] float _gravityScale = 10;
    public float gravityScale => _gravityScale;
}

