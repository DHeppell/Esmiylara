using Esmiylara.Enumerations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDirection = Esmiylara.Enumerations.Direction;

namespace Esmiylara.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class ActorController : MonoBehaviour
    {
        /// <summary>
        /// Defines actor momentum, this will move the actor.
        /// </summary>
        public Vector2 Momentum = Vector2.zero;
        /// <summary>
        /// The movement speed for actors. (This will be a function later.)
        /// </summary>
        public const float MovementSpeed = 2f;

        /// <summary>
        /// Defines the animator for the actor.
        /// </summary>
        private Animator animator;
        /// <summary>
        /// Defines the rigid body for the actor.
        /// </summary>
        private new Rigidbody2D rigidbody;

        /// <summary>
        /// Initializes local properties.
        /// </summary>
        protected virtual void Awake()
        {
            // Initialize components.
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        protected virtual void Start()
        {
            rigidbody.gravityScale = 0f;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            animator.runtimeAnimatorController = Resources.Load("Graphics/Animations/Actor") as RuntimeAnimatorController;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            // Check to see if momentum is set, if not then poll input.
            if (Momentum == Vector2.zero)
            {
                // Set the momentum to the walking direction.
                Momentum = Direction;
            }

            // Reset the speed.
            rigidbody.velocity = Vector2.zero;

            // Update the animator.
            UpdateAnimator();

            // Check to see if we have any momentum.
            if (Momentum != Vector2.zero)
            {
                // Apply the momentum.
                rigidbody.AddForce(Momentum, ForceMode2D.Impulse);

                // Clear momentum.
                Momentum = Vector2.zero;
            }
        }

        /// <summary>
        /// Updates the animator.
        /// </summary>
        protected virtual void UpdateAnimator()
        {
            // Get the direction as a direction enum value.
            var direction = DirectionExtensions.GetDirectionFromVector(Direction);

            // Check to see if we need to update the direction.
            if (direction != EDirection.None)
                animator.SetFloat("Direction", direction.AsByte());

            // Now send the properties to the animator.
            animator.SetFloat("MovementX", Momentum.x);
            animator.SetFloat("MovementY", Momentum.y);
            animator.SetBool("Moving", (Momentum != Vector2.zero));
        }

        /// <summary>
        /// Polls for movement input for the actor.
        /// </summary>
        protected virtual Vector2 Direction
        {
            get
            {
                // Return zero, we don't move with this class.
                return Vector2.zero;
            }
        }
    }
}