﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    public void FixedUpdate()
    {
        Vector2 direction = Vector3.up * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;




        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);





    }
}