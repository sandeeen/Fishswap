using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Textmover : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.localPosition += (Vector3)Random.insideUnitCircle;
        text.text = Random.Range(0, 10).ToString();
    }
}
