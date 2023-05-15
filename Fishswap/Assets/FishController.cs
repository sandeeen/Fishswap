using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{

    private Camera mainCamera;


    private void Awake()
    {
       
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        RaycastOnMousePos();

        UpdateFishPos();

    }

    private void RaycastOnMousePos()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    }

    private void UpdateFishPos()
    {
    }

}
