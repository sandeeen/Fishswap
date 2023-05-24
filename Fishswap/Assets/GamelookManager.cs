using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamelookManager : MonoBehaviour
{
    [SerializeField] GameObject grassObject;
    [SerializeField] GameObject dirtObject;

    public void ChangeToGrass()
    {
        grassObject.SetActive(true);
        dirtObject.SetActive(false);
    }

    public void ChangeToDirt()
    {
        grassObject.SetActive(false);
        dirtObject.SetActive(true);
    }


}
