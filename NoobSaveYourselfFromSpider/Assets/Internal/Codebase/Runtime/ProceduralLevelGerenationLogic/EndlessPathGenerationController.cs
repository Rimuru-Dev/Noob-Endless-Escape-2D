// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Codebase.Runtime.ProceduralLevelGerenationLogic
{
    public sealed class EndlessPathGenerationController : MonoBehaviour
    {
        public Transform root;
        public Transform spawnPoint;
        public GameObject[] prefabs;
        public float tileWidth;
        public float speed = -5;

        private List<GameObject> pool = new();

        private void Start()
        {
            Vector2 pos = spawnPoint.position;
            for (var i = 0; i < prefabs.Length; i++)
            {
                if (i == 0)
                {
                    var instance = Instantiate(prefabs[i], pos, Quaternion.identity, root);
                    pool.Add(instance);
                }
                else
                {
                    var instance = Instantiate(prefabs[i], root);
                    var position = new Vector2((i * instance.GetComponent<BoxCollider2D>().size.x) - pos.x, pos.y);

                    instance.transform.position = position;

                    pool.Add(instance);
                }
            }
        }

        private void Update()
        {
            for (var i = 0; i < pool.Count; i++)
            {
                if (pool[i] == null)
                    pool.TrimExcess();
                else
                    pool[i].transform.position = new Vector2(pool[i].transform.position.x + speed * Time.deltaTime,
                        pool[i].transform.position.y);
            }
        }

        private float GetTileWidth(GameObject tile)
        {
            BoxCollider2D collider = tile.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                return collider.bounds.size.x;
            }

            return 0f;
        }

        private float GetSizeX(GameObject target)
        {
            var boxCollider2D = target.GetComponent<BoxCollider2D>();

            return boxCollider2D != null ? boxCollider2D.size.x : 0f;
        }

        private float GeOffsetX(GameObject target)
        {
            var boxCollider2D = target.GetComponent<BoxCollider2D>();

            return boxCollider2D != null ? boxCollider2D.offset.x : 0f;
        }
    }
}