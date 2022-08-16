using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class stroker : MonoBehaviour
{
    private Transform startMarker;
    private Transform endMarker;
    

    public Transform[] Markers = new Transform[7];
    public Transform HiddenMarker;


    public float[] speeds;

    public float duration;
    // Movement speed in units per second.
    private float speed = 1.0F;

    public Vector3 lerpOffset;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    private int path = 0;
    private bool journeyComplete = false;
    private bool stroking = false;
    private float strokeStarted = 0F;
    private bool finished = false;


    public AnimationCurve MoveCurve;



    void Start()
    {
        SteamVR_Actions.default_GrabPinch.AddOnStateDownListener(TriggerPressed, SteamVR_Input_Sources.Any);
     
        journeyComplete = true;

        startMarker = transform;
        endMarker = Markers[1];

    }

    // Move to the target end position.
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (!stroking)
            {
                StartStrokingCycle();
            }
            else
            {
                PauseStroking();
            }
        }

      


        if (!journeyComplete&&!finished) { 
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;


            Vector3 positionOffset = new Vector3(0, 0, 0);
            
            if (path ==0) { positionOffset = MoveCurve.Evaluate(fractionOfJourney) * lerpOffset; }
            
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney)+positionOffset;

            // Get the current position of the sphere

 
            if ((transform.position == endMarker.position))
            {
                if (endMarker.position == HiddenMarker.position) { finished = true; }
                else
                {
                    journeyComplete = true;
                    if ((path == 0) && (strokeStarted == 0F)) { Debug.Log(Time.time); strokeStarted = Time.time; }
                    SwitchPath();
                    StartStroke();
                }

            }

            checkCountdown();
        }

     

    }

    private void checkCountdown()
    {
        if (Time.time - strokeStarted > duration) { Debug.Log(Time.time); Debug.Log("Stop!"); EndStroking(); }
    }

    private void EndStroking()
    {
            startMarker = transform;
            endMarker = HiddenMarker;
            path = -1;
            speed = 1F;
            StartStroke();
    }

    void StartStroke()
    {
        // Keep a note of the time the movement started.
        journeyComplete=false;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

    }

    void SwitchPath()
    {
        //if (path == 3) { Debug.Log(Time.time); }
        path++;
        if (path >= Markers.Length) { path = 0; }
        //       if (path >= 3 | path == 0) { speed = 3.3f; }
        //        if (path >=5) { speed = 1.5f; }
        //        if (path == 1) { speed = 1f; }

        //if (path >= 3 && path <5) { speed = 3.3f; }
        //else { speed = 1f; }

        speed = speeds[path];

        startMarker = endMarker;
        endMarker = Markers[path];

       // if (path == 1) { Debug.Log(Time.time); }
        
    }

    void StartStrokingCycle()
    {
        startMarker = transform;
        endMarker = Markers[0];
        stroking = true;
        StartStroke();
    }

    void PauseStroking()
    {
        journeyComplete = true;
        stroking = false;
        path = 0;
    }

    private void TriggerPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!stroking)
        {
            StartStrokingCycle();
        }
        else
        {
            PauseStroking();
        }
   
    }
}

