using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
//using UnityEngine.XR;

public class calibrater : MonoBehaviour
{
    public GameObject room;
    public GameObject body;
    public GameObject eyeCalibMark;
    public GameObject coverApparatus;
    public GameObject blanket;
    public GameObject hand;
    public GameObject blinds;
    public GameObject driftPlate;
    public GameObject driftCloth;
    public GameObject experimenter;

    private Mode mode;
    private enum Mode
    {   
        idle =0,
        closingBlinds = 1,
        movingDriftPlate = 2,
        openingBlinds = 3
    }

    private float closedZ = 0.495F;
    private float openedZ = 0.2F;
    private float shownRulerZ = 6.037F;

    // Start is called before the first frame update
    void Start()
    {
        SteamVR_Actions.default_GrabGrip.AddOnStateDownListener(OnSideBtn, SteamVR_Input_Sources.Any);
        //XRSettings.eyeTextureResolutionScale = 2f;
    }

    private void OnSideBtn(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        mode = Mode.closingBlinds;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

            ToggleObject(room);
            ToggleObject(body);
            ToggleObject(eyeCalibMark);
            ToggleObject(coverApparatus);
            ToggleObject(hand);
            ToggleObject(blanket);

        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            blinds.gameObject.transform.localScale -= new Vector3(0, 0, 0.005F);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            blinds.gameObject.transform.localScale += new Vector3(0, 0, 0.005F);
        }

        if (Input.GetKey(KeyCode.D))
        {
            mode = Mode.closingBlinds;
        }

        if (mode == Mode.closingBlinds)
        {
            experimenter.SetActive(false);

            Vector3 target = new Vector3(blinds.gameObject.transform.localScale.x, blinds.gameObject.transform.localScale.y, closedZ);
            blinds.gameObject.transform.localScale = Vector3.Lerp(blinds.gameObject.transform.localScale, target, Time.deltaTime/ Vector3.Distance(blinds.gameObject.transform.localScale, target)*0.2f);
            if (blinds.gameObject.transform.localScale.z == closedZ)
            {
    
                mode = Mode.movingDriftPlate;
            }
        } 
        else if (mode == Mode.movingDriftPlate)
        {
            driftCloth.SetActive(true);

            Vector3 target = new Vector3(driftPlate.transform.localPosition.x, driftPlate.transform.localPosition.y, shownRulerZ);
            driftPlate.transform.localPosition = Vector3.Lerp(driftPlate.transform.localPosition, target, Time.deltaTime / Vector3.Distance(driftPlate.transform.localPosition, target) * 0.8f);
            if (driftPlate.transform.localPosition.z <= shownRulerZ)
            {

                mode = Mode.openingBlinds;
            }
        }
        else if (mode == Mode.openingBlinds)
        {
            Vector3 target = new Vector3(blinds.gameObject.transform.localScale.x, blinds.gameObject.transform.localScale.y, openedZ);
            blinds.gameObject.transform.localScale = Vector3.Lerp(blinds.gameObject.transform.localScale, target, Time.deltaTime / Vector3.Distance(blinds.gameObject.transform.localScale, target) * 0.2f);
            if (blinds.gameObject.transform.localScale.z == openedZ)
            {

                mode = Mode.idle;
            }
        }
    }

    void ToggleObject(GameObject obj)
    {
        if (obj.activeSelf)
        {
            obj.SetActive(false);
        }
        else 
        {
            obj.SetActive(true);
        }


    }
}
