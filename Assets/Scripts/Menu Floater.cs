using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFloater : MonoBehaviour
{
    private Transform mainCam;
    public float yOffset;
    public GameObject target;
    public OVRSkeleton skeleton;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main.transform;
        skeleton = GetComponentInChildren<OVRSkeleton>() ?? skeleton;
    }

    // Update is called once per frame
    void Update()
    {
        if (skeleton.IsInitialized)
        {
            OVRBone wristBone = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_WristRoot];
            Vector3 wristPosition = wristBone.Transform.position;

            // Position the gameobject above the wrist
            transform.position = new Vector3(wristPosition.x, wristPosition.y + yOffset, wristPosition.z);

            // Make the gameobject face the user
            transform.LookAt(mainCam);
            transform.RotateAround(transform.position, transform.up, 180f);
        }
        else
        {
            Debug.Log("Skeleton not initialized");
        }
    }
}
