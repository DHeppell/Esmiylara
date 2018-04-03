using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Esmiylara.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public GameObject Target;

        // Use this for initialization
        void Start()
        {
            Target = GameObject.Find("Player");
        }

        // Update is called once per frame
        void Update()
        {
            Camera.main.orthographicSize = (Screen.height / 2) / Constants.Graphics.PixelsPerUnit;

            if (Target != null)
            {
                transform.position = new Vector3(
                    Target.transform.position.x, Target.transform.position.y, -10);
            }
            else
            {
                transform.position = new Vector3(0, 0, -10);
            }
        }
    }
}