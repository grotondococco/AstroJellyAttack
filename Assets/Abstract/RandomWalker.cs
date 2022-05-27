using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public abstract class RandomWalker:MonoBehaviour
    {
        protected float upBound;
        protected float downBound;  
        protected float leftBound;
        protected float rightBound;
        protected float walkSpeed;
        protected float randomWalkCooldown;
        protected bool isWalking = false;

        protected abstract void SetParameters();
        protected Vector3 GenerateRandomVersor()
        {
            Vector3 res = Vector3.zero;
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            res = new Vector3(randomX, randomY, 0);
            if ((randomX == 0 && randomY == 0)) res = GenerateRandomVersor();
            return res;
        }
        protected int IsAtTheBounds()
        {
            if (gameObject.transform.position.x <= leftBound) return 0;
            if (gameObject.transform.position.y >= upBound) return 1;
            if (gameObject.transform.position.x >= rightBound) return 2;
            if (gameObject.transform.position.y <= downBound) return 3;
            return -1;
        }

        protected Vector3 PickOppositeVectorFromDirection(int direction)
        {
            Vector3 res = Vector3.zero;
            if (direction == 0) res = Vector3.right * 0.5f; ;
            if (direction == 1) res = Vector3.down * 0.5f;
            if (direction == 2) res = Vector3.left * 0.5f;
            if (direction == 3) res = Vector3.up * 0.5f;
            return res;
        }

         protected abstract IEnumerator WalkRandom(float randomWalkCooldownSeconds);
         protected abstract IEnumerator Walk(float speed, Vector3 direction);
}
