﻿using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public Player Player;
        public float Speed = 3f;
        public float Health = 5;

        public GameObject ExplosionGameObject;

        void OnTriggerEnter(Collider col)
        {
            var bullet = col.GetComponent<Bullet>();
            if (bullet != null)
            {
                Health -= bullet.Damage;

                EventAggregator.SendMessage(new SpawnDamageTextMessage { Position = transform.position, Text = bullet.Damage.ToString("N0") });

                if (Health > 0)
                {
                    EventAggregator.SendMessage(new SpawnGibsMessage { Count = 1, Position = transform.position });
                    EventAggregator.SendMessage(new SpawnBloodMessage { Position = transform.position });
                }
                else
                {
                    Destroy(gameObject);
                    EventAggregator.SendMessage(new SpawnBloodMessage { Position = transform.position, SplatterSize = SplatterSize.Medium });
                    EventAggregator.SendMessage(new SpawnGibsMessage { Count = 5, Position = transform.position });
                }
            }
        }
    }
}