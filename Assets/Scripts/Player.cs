using UnityEngine;
using System;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public float Speed;
        public float Sensitivity;
        private float DefaultFOV;

        void Start()
        {
            DefaultFOV = Camera.main.fieldOfView;
        }

        void Update()
        {
            var frameSpeed = Speed * Time.deltaTime;

            var horizontalSpeed = Input.GetAxisRaw("Horizontal")*frameSpeed;
            var verticalSpeed = Input.GetAxisRaw("Vertical")*frameSpeed;
            if (horizontalSpeed < 0.1f && verticalSpeed < 0.1f)
            {
                //var animator = GetComponentInChildren<Animator>();
                //animator.SetFloat("speed", 0f);
            }
            else
            {
                var animator = GetComponentInChildren<Animator>();
                animator.SetFloat("speed", 2f);
            }
            transform.Translate(horizontalSpeed, verticalSpeed, 0);

            if (Input.GetMouseButton(0))
            {
                var gun = GetComponent<Gun>();
                if (gun != null) gun.Fire();
            }

            var fov = Camera.main.fieldOfView;
            Camera.main.fieldOfView = Mathf.Clamp(fov + Input.GetAxis("Mouse ScrollWheel") * Sensitivity, DefaultFOV - 20, DefaultFOV + 30);
        }
    }
}