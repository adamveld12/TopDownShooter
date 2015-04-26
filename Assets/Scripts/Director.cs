﻿using Assets.Scripts.Util;
using Dungeon.Generator;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Director : MonoBehaviour
    {
        public GameObject Enemy;
        public float SpawnDelay = 5;
        public float CurrentSpawnDelay;

        public GameObject Tile;
        public GameObject Wall;
        public GameObject Exit;
        public GameObject Mob;
        public float TileSize = 8;

        void Start()
        {
            CurrentSpawnDelay = SpawnDelay;
            CreateLevel();
        }

        private void CreateLevel()
        {
            var map = Generator.Generate(MapSize.Small, (uint)Random.Range(1, 255));

            for (var y = 0; y < map.Height; y++)
            {
                for (var x = 0; x < map.Width; x++)
                {
                    var mapTile = map[x, y];
                    var tilePosition = new Vector3(x, y) * TileSize;

                    // tile type
                    if (mapTile.MaterialType == MaterialType.Floor)
                    {
                        var tile = Instantiate(Tile);
                        tile.transform.position = tilePosition;

                        if (Random.Range(0, 5) == 0)
                        {
                            var mob = Instantiate(Mob);
                            mob.transform.position = tilePosition;
                        }
                    }
                    else if (mapTile.MaterialType == MaterialType.Wall)
                    {
                        var wall = Instantiate(Wall);
                        wall.transform.position = tilePosition;
                    }

                    // tile attributes
                    if (mapTile.Attributes.HasFlag(AttributeType.Entry))
                    {
                        var player = FindObjectOfType<Player>();
                        player.transform.position = tilePosition;
                    }
                    else if (mapTile.Attributes.HasFlag(AttributeType.Exit))
                    {
                        var exit = Instantiate(Exit);
                        exit.transform.position = tilePosition;
                    }

//                    if (mapTile.Attributes.HasFlag(AttributeType.MobSpawn))
//                    {
//                        var mob = Instantiate(Mob);
//                        mob.transform.position = tilePosition;
//                    }
                }
            }
        }

        private Vector3 GetOffscreenPosition()
        {
            var spawnSide = Random.Range(0, 4);
            const float width = 19f;
            const float height = 11f;

            Vector3 offset;

            if (spawnSide == 0) offset = new Vector3(-width, Random.Range(-height, height), 0);
            else if (spawnSide == 1) offset = new Vector3(Random.Range(-width, width), height, 0);
            else if (spawnSide == 2) offset = new Vector3(width, Random.Range(-height, height), 0);
            else offset = new Vector3(Random.Range(-width, width), -height, 0);

            return Camera.main.transform.position.ToXY() + offset;
        }
    }
}