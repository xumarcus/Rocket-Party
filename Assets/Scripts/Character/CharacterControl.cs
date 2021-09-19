﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControl : MonoBehaviour
{
    [SerializeField] protected float characterSpeed;
    [SerializeField] protected float rocketCooldownDuration;
    [SerializeField] protected GameObject rocketClass;
    [SerializeField] protected Transform spawnPos;
    [SerializeField] protected Rigidbody2D rigidBody;
    protected float rocketCooldownTimer = 0.0f;

    public abstract void TakeDamage();

    public bool FireWeapon(Vector3 targetPosition)
    {
        if (rocketCooldownTimer > 0.0f)
        {
            return false;
        }
        rocketCooldownTimer = rocketCooldownDuration;
        var angle = Vector2.SignedAngle(Vector2.up, targetPosition - transform.position);
        var rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        Instantiate(rocketClass, transform.position, rotation);
        return true;
    }
}
