﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speedcamera;

    public float speedzoom;

    public Camera camera;

    public float zoom;

    public bool activated;
    // Update is called once per frame

    private void Start()
    {
        activated = false;
    }

    void Update()
    {
        if (activated)
        {
            int Z = Convert.ToInt32(Input.GetKey("z")) - Convert.ToInt32(Input.GetKey("s"));
            int X = Convert.ToInt32(Input.GetKey("d")) - Convert.ToInt32(Input.GetKey("q"));
            float Y = Convert.ToSingle(Input.GetAxis("Mouse ScrollWheel"));
            transform.position += transform.up * speedcamera * Z + transform.right * speedcamera * X;
            camera.orthographicSize -= Y * speedzoom;
            if (camera.orthographicSize < 1)
                camera.orthographicSize = 1;
            speedcamera = camera.orthographicSize * 0.025f;
            speedzoom = camera.orthographicSize;
        }
    }
}