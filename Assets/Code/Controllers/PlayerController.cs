using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Esmiylara.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        public float MovementSpeed = 2f;

        private Animator animator;
        private new Rigidbody2D rigidbody;
        private new BoxCollider2D collider;

        [SerializeField]
        private Vector2 force;

        void Awake()
        {
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody2D>();
            collider = GetComponent<BoxCollider2D>();
        }

        // Use this for initialization
        void Start()
        {
            rigidbody.gravityScale = 0f;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // Update is called once per frame
        void Update()
        {
            CheckInput();
        }

        void CheckInput()
        {
            force = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            CalculateMovement(force * MovementSpeed);
        }

        void CalculateMovement(Vector2 force)
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}