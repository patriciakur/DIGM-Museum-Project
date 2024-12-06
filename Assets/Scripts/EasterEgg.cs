using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    public bool CoogLeftSelected = false;
    public bool CoogRightSelected = false;
    public GameObject Liao;
    public GameObject[] otherItems;

    public void CoogLeftSelectedTrue()
    {
        CoogLeftSelected = true;
    }
    public void CoogLeftSelectedFalse()
    {
        CoogLeftSelected = false;
    }
    public void CoogRightSelectedTrue()
    {
        CoogRightSelected = true;
    }
    public void CoogRightSelectedFalse()
    {
        CoogRightSelected = false;
    }

    void Update()
    {
        if (CoogLeftSelected && CoogRightSelected)
        {
            Debug.Log("Easter Egg Activated!");
            foreach (GameObject item in otherItems)
            {
                item.SetActive(false);
            }
            Liao.SetActive(true);
        }
        else
        {
            Liao.SetActive(false);
        }
    }
}
