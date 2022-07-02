using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies {
    public class Spawner : MonoBehaviour {
        [SerializeField] private GameObject toSpawn;

        [SerializeField] private Transform target;
        [SerializeField] private Light lightSource;

        [SerializeField] private float maxSpawnDistance;
        [SerializeField] private float spawnTimeDelay;

        [SerializeField] [Range(1, 100)] private int maxMobs;

        // private GameObject[] mobs;

        public Vector3 GeneratePosition() {
            var lightRange = lightSource.range;

            var targetPosition = target.position;
            
            var xTarget = targetPosition.x;
            var yTarget = targetPosition.y;

            // var angle = Mathf.PI * 2f / Random.Range(0f, 360f);
            var angle = Random.Range(0f, 359f) * Mathf.PI / 180;
            var cos = Mathf.Cos(angle);
            var sin = Mathf.Sin(angle);
            
            var x = Random.Range(xTarget, xTarget + cos * (maxSpawnDistance - lightRange)) + cos * lightRange;
            var y = Random.Range(yTarget, yTarget + sin * (maxSpawnDistance - lightRange)) + sin * lightRange;

            return new Vector3(
                x, // wqaaaaaaaaaaa                 ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccxccccccccccccccccccccccccccccccccccccccccdfx, // clamp edge of map
                y,
                -1
            );
        }

        public void Spawn() {
            Instantiate(toSpawn, GeneratePosition(), Quaternion.identity);
        }

        // Start is called before the first frame update
        void Start() {
            InvokeRepeating("Spawn", 0, spawnTimeDelay);
        }

        // Update is called once per frame
        void Update() { }
    }
}
