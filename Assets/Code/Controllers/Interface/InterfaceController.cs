using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Esmiylara.Enumerations;
using EMouseButton = Esmiylara.Enumerations.MouseButton;
using System;

namespace Esmiylara.Controllers.Interface
{
    /// <summary>
    /// Interface controller, defines basic interface functionality.
    /// </summary>
    public class InterfaceController : MonoBehaviour
    {
        [SerializeField]
        protected bool[] Mouse;
        /// <summary>
        /// Tells unity if the object is enabled.
        /// </summary>
        public bool Enabled = true;
        /// <summary>
        /// Caches the original alpha value.
        /// </summary>
        private float originalAlpha;

        /// <summary>
        /// Called when the controller is initialized.
        /// </summary>
        void Start()
        {
            // Initialize the mouse button cache.
            var btnCount = Enum.GetValues(typeof(EMouseButton)).Length;
            Mouse = new bool[btnCount];

            // Get the renderer.
            var renderer = GetComponent<CanvasRenderer>();

            // Check to see if we got it.
            if (renderer != null)
            {
                // Set the original alpha value.
                originalAlpha = renderer.GetAlpha();
            }
        }

        /// <summary>
        /// Called when the controller is updated.
        /// </summary>
        void Update()
        {
            // Check for mouse input.
            DetectMouseButtons();
        }

        /// <summary>
        /// Changes the component's alpha value.
        /// </summary>
        /// <param name="value">The new alpha value.</param>
        public virtual void SetAlpha(float value)
        {
            // Store the new value.
            originalAlpha = value;

            // Force an update.
            Visible = true;
        }

        /// <summary>
        /// Detects mouse inputs.
        /// </summary>
        private void DetectMouseButtons()
        {
            // Loop through the enumeration and update all buttons.
            foreach (EMouseButton button in Enum.GetValues(typeof(EMouseButton)))
            {
                // Call the mouse button input function.
                MouseButton(button);
            }
        }

        /// <summary>
        /// The interface object's position.
        /// </summary>
        public virtual Vector2 Position
        {
            get
            {
                // Try to get a Transform object.
                var transform = GetComponent<Transform>();

                // Check to see if we got an object.
                if (transform != null)
                {
                    // We did, return the value.
                    return new Vector2(transform.position.x, transform.position.y);
                }

                // We did not, return zero.
                return Vector2.zero;
            }
            set
            {
                // Try to get a Transform object.
                var transform = GetComponent<Transform>();

                // Check to see if we got an object.
                if (transform != null)
                {
                    // We did, set the values.
                    transform.position = new Vector3(value.x, value.y, transform.position.z);
                }
            }
        }

        /// <summary>
        /// The interface object's size.
        /// </summary>
        public virtual Vector2 Size
        {
            get
            {
                // Try to get a RectTransform object.
                var transform = GetComponent<RectTransform>();

                // Check to see if we got an object.
                if (transform != null)
                {
                    // We did, return the value.
                    return new Vector2(transform.rect.width, transform.rect.height);
                }

                // We did not, return zero.
                return Vector2.zero;
            }
            set
            {
                // Try to get a RectTransform object.
                var transform = GetComponent<RectTransform>();

                // Check to see if we got an object.
                if (transform != null)
                {
                    // We did, update the values.
                    transform.sizeDelta = value;
                }
            }
        }

        /// <summary>
        /// Tells unity if the object is visible.
        /// </summary>
        public bool Visible
        {
            get
            {
                // Get unity's renderer object.
                var renderer = GetComponent<CanvasRenderer>();

                // Check to see if we got the object.
                if(renderer != null)
                {
                    // we did, return the value.
                    return (renderer.GetAlpha() > 0);
                }

                // We did not, return false.
                return false;
            }
            set
            {
                // Get unity's renderer object.
                var renderer = GetComponent<CanvasRenderer>();

                // Check to see if we got the object.
                if (renderer != null)
                {
                    // we did, update the value.
                    renderer.SetAlpha((value) ? originalAlpha : 0);
                }

                // Loop through the child objects.
                for (int i = 0; i < transform.childCount; i++)
                {
                    // Get the current child.
                    var child = transform.GetChild(i);

                    // Make sure it is loaded.
                    if (child != null)
                    {
                        // Get the interface controller.
                        var controller = child.GetComponent<InterfaceController>();

                        // Make sure it is loaded.
                        if(controller != null)
                        {
                            // Set visibility.
                            controller.Visible = value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The interface object's internal mouse position.
        /// </summary>
        public virtual Vector2 MousePosition
        {
            get
            {
                // Subtract the object's position from the mouse's position.
                return new Vector2(Input.mousePosition.x, Input.mousePosition.y) -
                    new Vector2(transform.position.x, transform.position.y);
            }
        }

        /// <summary>
        /// The interface object's bounding rectangle.
        /// </summary>
        public virtual Rect Bounds
        {
            get
            {
                // Return the rectangle.
                return new Rect(Position, Size);
            }
        }

        /// <summary>
        /// Checks to see if the mouse is in bounds.
        /// </summary>
        public virtual bool IsInBounds
        {
            get
            {
                // Check the Y axis.
                if (MousePosition.y >= 0 && MousePosition.y <= Bounds.height)
                {
                    // Check the X axis.
                    if (MousePosition.x >= 0 && MousePosition.x <= Bounds.width)
                    {
                        // It is, yay!
                        return true;
                    }
                }

                // It is not, boo!
                return false;
            }
        }

        /// <summary>
        /// Checks to see if a mouse button is pressed.
        /// </summary>
        /// <param name="button">The mouse button to check.</param>
        /// <returns>The state of the button.</returns>
        public virtual bool MouseButton(EMouseButton button)
        {
            // Get the mouse button state.
            var value = Input.GetMouseButton(button.AsByte());

            // Validate the object.
            if (Visible && Enabled && IsInBounds)
            {
                // Update the button they are using.
                Mouse[button.AsByte()] = value;

                // Return the value.
                return value;
            }
            else
            {
                // Update the button they are using.
                if (Mouse[button.AsByte()])
                    Mouse[button.AsByte()] = value;
            }

            // Not validated, return false.
            return false;
        }
    }
}