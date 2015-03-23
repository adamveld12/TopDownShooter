﻿using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public Player Player;
        public float Speed = 3f;
        public int Health = 5;

        private bool _isDead;

        void Update()
        {
            if (_isDead) return;

            if (Player == null)
            {

                Player = FindObjectOfType<Player>();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position,
                    Speed*Time.deltaTime);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<Bullet>() != null)
            {
                if (Health > 0) Destroy(col.gameObject);

                Health--;
                
                if (Health <= 0) Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            //Spawn blood
        }
    }
}