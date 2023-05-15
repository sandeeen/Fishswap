using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;


    private Camera mainCamera;
    private Vector3 targetPos;

    float mouseX;
    float mouseY;

    private void Awake()
    {

    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (mouseX != 0f || mouseY != 0f)
        {
            RaycastOnMousePos();

        }

        UpdateFishPos();


    }

    private void RaycastOnMousePos()
    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            targetPos = hit.point;

        }
        else
        {
            Debug.Log("Raycast did not hit");
        }
    }

    private void UpdateFishPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.LookAt(targetPos);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);
    }

}
