using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Esmiylara.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class NPCController : ActorController
    {
        private float tickMove = 0;
        private Vector2 direction = Vector2.zero;

        protected override Vector2 Direction
        {
            get
            {
                tickMove--;

                if(tickMove <= 0)
                {
                    if (direction == Vector2.zero)
                        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    else
                        direction = Vector2.zero;

                    tickMove = Random.Range(125, 500);
                }

                return direction;
            }
        }

        /// <summary>
        /// Defines the actor sprite. (This will be a function later.)
        /// </summary>
        public override string Sprite
        {
            get
            {
                return "actor-npc";
            }
        }
    }
}