using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AsteroidBelt.Enemies_scripts.Enemy_Behaviours
{
    public class SquadMinions : EnemyHitHandler
    {
        public float speed;
        private Vector3 positionToAttackFrom;
        private bool goingLeft, moving;

        public void SetAttackPosition(Vector2 destination, bool leftOrNot){
            positionToAttackFrom = destination;
            goingLeft = leftOrNot;
            MoveToPosition();
            StartCoroutine(LifeTime());
            StartCoroutine(DelayBeforeAttack());
        }

        private void MoveToPosition(){

            Vector3 intermediatePoint = Vector3.zero;
            Vector3 controlPointA = Vector3.zero;
            Vector3 controlPointB = Vector3.zero;
            Vector3 controlPointC = Vector3.zero;
            Vector3 controlPointD = Vector3.zero;

            if(goingLeft){
                intermediatePoint = new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z);
                controlPointA = new Vector3(transform.position.x - 8, intermediatePoint.y, intermediatePoint.z);
                controlPointB = new Vector3(intermediatePoint.x, intermediatePoint.y + 8, intermediatePoint.z);
                controlPointC = new Vector3(intermediatePoint.x, intermediatePoint.y - 8, intermediatePoint.z);
                controlPointD = new Vector3(positionToAttackFrom.x, positionToAttackFrom.y + 8, positionToAttackFrom.z);
            }
            else{
                intermediatePoint = new Vector3(transform.position.x + 10f, transform.position.y, transform.position.z);
                controlPointA = new Vector3(transform.position.x + 8, intermediatePoint.y, intermediatePoint.z);
                controlPointB = new Vector3(intermediatePoint.x, intermediatePoint.y + 8, intermediatePoint.z);
                controlPointC = new Vector3(intermediatePoint.x, intermediatePoint.y - 8, intermediatePoint.z);
                controlPointD = new Vector3(positionToAttackFrom.x, positionToAttackFrom.y + 8, positionToAttackFrom.z);
            }

            Vector3[] pathWaypoints = new[]{intermediatePoint, controlPointA, controlPointB, positionToAttackFrom, controlPointC, controlPointD};
            transform.DOPath(pathWaypoints, 2.5f, PathType.CubicBezier, PathMode.Full3D, 10, Color.yellow);
        }

        private IEnumerator DelayBeforeAttack(){
            yield return new WaitForSeconds(3f);
            moving = true;
        }

        private IEnumerator LifeTime(){
            yield return new WaitForSeconds(15f);
            Destroy(gameObject);
        }

        private void Update(){
            if(moving){
                if(goingLeft){
                    transform.position += transform.right * speed * Time.deltaTime;
                }
                else{
                    transform.position += -transform.right * speed * Time.deltaTime;
                }
            }
        }
    }
}
