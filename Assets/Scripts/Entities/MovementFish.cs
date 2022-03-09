using System.Collections;
using UnityEngine;
using Objects;
using System.Collections.Generic;

namespace Entities
{
    public class MovementFish : MonoBehaviour
    {
        private float _moveSpeed = 1f;
        private Vector2 _randomPos;
        private SpriteRenderer _fishSprite;

        private List<DestroyInEndScene> _snacksOnScene = new List<DestroyInEndScene>();

        void Awake()
        {
            _fishSprite = GetComponent<SpriteRenderer>();
            StartCoroutine(IEGenerateRandomPos());
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            DestroyInEndScene snackAlive;

            if (_snacksOnScene.Count > 0)
            {
                snackAlive = _snacksOnScene[0];
            }
            else
            {
                snackAlive = null;
            }


            if (snackAlive != null && !snackAlive.gameObject.CompareTag("Remedy"))
            {
                _moveSpeed = 3f;
                transform.position = Vector2.MoveTowards(transform.position, snackAlive.transform.position, _moveSpeed * Time.fixedDeltaTime);

                if (transform.position.x > snackAlive.transform.position.x)
                {
                    _fishSprite.flipX = false;
                }
                else
                {
                    _fishSprite.flipX = true;
                }


            }
            else
            {
                _snacksOnScene.Clear();
                _moveSpeed = 1f;
                transform.position = Vector2.MoveTowards(transform.position, _randomPos, _moveSpeed * Time.fixedDeltaTime);
                if (transform.position.x > _randomPos.x)
                {
                    _fishSprite.flipX = false;
                }
                else
                {
                    _fishSprite.flipX = true;
                }
            }


        }

        private IEnumerator IEGenerateRandomPos()
        {
            _snacksOnScene.AddRange(FindObjectsOfType<DestroyInEndScene>());
            _randomPos.x = Random.Range(-7f, 7f);
            _randomPos.y = Random.Range(-1.85f, 2.5f);
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            StartCoroutine(IEGenerateRandomPos());
        }
    }
}