using Esmiylara;
using Esmiylara.Controllers.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            var hud = GameObject.Find("HUD-Vitals");
            var controller = hud.GetComponent<InterfaceController>();

            controller.Visible = false;
        }

        if (Input.GetKeyDown("2"))
        {
            var hud = GameObject.Find("HUD-Vitals");
            var controller = hud.GetComponent<InterfaceController>();

            controller.Visible = true;
        }
    }
}
