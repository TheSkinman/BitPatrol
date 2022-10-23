using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slammer : MonoBehaviour
{
    public Transform theSlammer, slammerTarget;
    public float slamSpeed, waitAfterSlam, resetSpeed;

    private Vector3 resetTarget;
    private float waitCounter;
    private bool hasTriggered, resetting;

    // Start is called before the first frame update
    void Start()
    {
        resetTarget = theSlammer.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
        }
        else
        {
            if (hasTriggered)
            {
                theSlammer.position = Vector3.MoveTowards(theSlammer.position, slammerTarget.position, slamSpeed * Time.deltaTime);

                if (Vector3.Distance(theSlammer.position, slammerTarget.position) <= .1f)
                {
                    waitCounter = waitAfterSlam;
                    hasTriggered = false;
                    resetting = true;
                }
            }
            else if (resetting)
            {
                theSlammer.position = Vector3.MoveTowards(theSlammer.position, resetTarget, resetSpeed * Time.deltaTime);

                if (Vector3.Distance(theSlammer.position, resetTarget) <= .1f)
                {
                    resetting = false;
                }
            }
            else
            {
                if (Vector3.Distance(slammerTarget.position, PlayerController.instance.transform.position) < 2f)
                {
                    hasTriggered = true;
                }
            }
        }
    }
}
