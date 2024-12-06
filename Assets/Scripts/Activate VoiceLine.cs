using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateVoiceLine : MonoBehaviour
{
    public GameObject pedestrianParent;

    public void PlayActiveChildrenAudio()
    {
        foreach (Transform child in pedestrianParent.transform)
        {
            if (child.gameObject.activeSelf)
            {
                AudioSource audioSource = child.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    child.GetComponent<BasicObjectScript>().ActivateObject();
                }
            }
        }
    }
}
