using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingThroughSpaceController : MonoBehaviour
{
    public float flyingSpeed = 15;
    private float rotationSpeed = 30f;

    private Vector3 rotationAxis;

    TitlescreenManager titlescreenManager;

    void Start()
    {
        titlescreenManager = GameObject.Find("Manager").GetComponent<TitlescreenManager>();

        rotationAxis = Random.onUnitSphere;

        Destroy(gameObject, 40f);   
    }

    void Update()
    {
        UpdateRotation();
        UpdatePosition();
    }

    void UpdatePosition()
    {
        transform.position += new Vector3(flyingSpeed, 0, 0) * Time.deltaTime;
    }

    void UpdateRotation()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

    
}
