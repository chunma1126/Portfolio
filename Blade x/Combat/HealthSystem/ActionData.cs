﻿using UnityEngine;



public struct ActionData
{
    public Vector3 hitPoint;
    public Vector3 hitNormal;
    public float damageAmount;
    public bool stun;
    
    public Vector3 knockbackDirection;
    public float knockbackForce;
    
    public ActionData( Vector3 hitPoint,  Vector3 hitNormal, float damageAmount, bool stun, Vector3 knockbackDirection = default, float knockbackForce = 0f )
    {
        this.hitPoint = hitPoint;
        this.hitNormal = hitNormal;
        this.damageAmount = damageAmount;
        this.stun = stun;
        
        this.knockbackDirection = knockbackDirection;
        this.knockbackForce = knockbackForce;
    }
    
}