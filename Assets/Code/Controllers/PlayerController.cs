using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Esmiylara.Controllers
{
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
    }
}