using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Esmiylara.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : ActorController
    {
        /// <summary>
        /// Polls for movement input for the player.
        /// </summary>
        protected override Vector2 Direction
        {
            get
            {
                return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * MovementSpeed;
            }
        }

        /// <summary>
        /// Defines the actor sprite. (This will be a function later.)
        /// </summary>
        public override string Sprite
        {
            get
            {
                return "actor-player";
            }
        }
    }
}