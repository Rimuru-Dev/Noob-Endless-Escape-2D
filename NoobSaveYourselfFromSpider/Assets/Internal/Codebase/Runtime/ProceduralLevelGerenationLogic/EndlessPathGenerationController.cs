// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.ProceduralLevelGerenationLogic
{
    public sealed class EndlessPathGenerationController : MonoBehaviour
    {
        public Transform root;
        public GameObject[] prefabs;
        public float tileWidth;

        private void Start()
        {
            tileWidth = GetTileWidth(prefabs[0]);

            var instance_1 = Instantiate(prefabs[0], Vector3.zero, Quaternion.identity, root);
            var instance_2 = Instantiate(prefabs[2], root);

            var collider_1 = instance_1.GetComponent<BoxCollider2D>();
            var collider_2 = instance_2.GetComponent<BoxCollider2D>();

            var pos = collider_1.size - collider_2.size;

            instance_2.transform.position = pos;

          //  instance_2.transform.position = new Vector2(collider_1.offset.y + collider_2.offset.y, instance_2.transform.position.y);


            // // // // 

            //   var instance_3 = Instantiate(prefabs[2], new Vector2(collider_1.offset.y + collider_2.offset.y, instance_2.transform.position.y), Quaternion.identity, root);
            // var collider_3 = instance_3.GetComponent<BoxCollider2D>();
            //
            // var newPos = collider_2.size - collider_3.size;
            //
            // instance_3.transform.position = newPos;
            //
            // instance_3.transform.position = new Vector2(collider_2.offset.y + collider_3.offset.y, instance_3.transform.position.y);
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
    }
}