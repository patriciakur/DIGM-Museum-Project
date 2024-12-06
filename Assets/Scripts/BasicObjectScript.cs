using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Oculus.Platform;
using UnityEngine;

public class BasicObjectScript : MonoBehaviour
{
    public GameObject LeftHand;
    public GameObject RightHand;

    public void ActivateObject()
    {
        // Deactivate Grabbable and HandGrabInteractable components
        var grabbable = GetComponent<Oculus.Interaction.Grabbable>();
        if (grabbable != null)
        {
            
            grabbable.enabled = false;
        }
        var handGrabInteractable = GetComponent<Oculus.Interaction.HandGrab.HandGrabInteractable>();
        if (handGrabInteractable != null)
        {
            handGrabInteractable.enabled = false;
        }
        LeftHand.GetComponent<HandGrabInteractor>().Unselect();
        RightHand.GetComponent<HandGrabInteractor>().Unselect();
        //floats the object to the pedestal / starts rotating slowly
        StartCoroutine(FloatToObject(new Vector3(0, 0, 0)));

        IEnumerator FloatToObject(Vector3 targetPosition)
        {
            float duration = 2.0f; // Duration of the float
            yield return new WaitForSeconds(1.0f); // Wait for a second before starting the rotation back
            StartCoroutine(RotateTo0Degrees());
            Vector3 startPosition = transform.position;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
                StartCoroutine(RotateObject(audioSource));
                yield return new WaitForSeconds(audioSource.clip.length);
 
                // Reset position and rotation of object
                StartCoroutine(RotateTo0Degrees());
                if (grabbable != null)
                {
                    grabbable.enabled = true;
                }
                if (handGrabInteractable != null)
                {
                    handGrabInteractable.enabled = true;
                }

                
            }
        }

        IEnumerator RotateObject(AudioSource audioSource)
        {
            while (audioSource.isPlaying)
            {
                transform.Rotate(new Vector3(0, 0.5f, 0), Space.World);
                yield return null;
            }
        }

        IEnumerator RotateTo0Degrees()
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            Quaternion startRotation = transform.rotation;
            float duration = 2.0f; // Duration of the rotation
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = targetRotation;
        }
    }

    public void InstantFaceForward()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
