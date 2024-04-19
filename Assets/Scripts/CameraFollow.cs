using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(-0.4f, 0.0f, -10f);
    public float dampingTime = 0.3f;
    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Application.targetFrameRate = 60; //60 frames x seg
    }

    void Start()
    {
        
    }

    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    void MoveCamera(bool smooth) {

        Vector3 destination = new Vector3(
            target.position.x - offset.x,
            offset.y,
            offset.z );

        if( smooth )
        {
            this.transform.position = Vector3.SmoothDamp(
                this.transform.position,
                destination,
                ref velocity,
                dampingTime
                );
        }
        else
        {
            this.transform.position = destination;
        }
    }
}

/*
 SmoothDamp( posicion_de_la_camara, objetivo_a_donde_ir, parametro_por_referencia_velocidad, tiempo);
 */