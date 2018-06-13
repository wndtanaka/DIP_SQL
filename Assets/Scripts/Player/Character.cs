using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public abstract class Character : MonoBehaviour
    {

        [SerializeField]
        float speed;

        protected Animator anim;

        protected Vector2 direction;

        Rigidbody2D rigid;

        public bool IsMoving
        {
            get
            {
                return direction.x != 0 || direction.y != 0;
            }
        }

        protected bool isAttacking = false;
        protected Coroutine attackRoutine;
        // Use this for initialization
        protected virtual void Start()
        {
            anim = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody2D>();

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            HandleLayers();
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            rigid.velocity = direction.normalized * speed;
        }

        public void HandleLayers()
        {
            if (IsMoving)
            {
                ActivateLayer("Walk");

                anim.SetFloat("X", direction.x);
                anim.SetFloat("Y", direction.y);
                StopAttack();
            }
            else if (isAttacking)
            {
                ActivateLayer("Attack");
            }
            else
            {
                ActivateLayer("Idle");
            }
        }

        public void ActivateLayer(string layerName)
        {
            for (int i = 0; i < anim.layerCount; i++)
            {
                anim.SetLayerWeight(i, 0);
            }
            anim.SetLayerWeight(anim.GetLayerIndex(layerName), 1);
        }

        public virtual void StopAttack()
        {
            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                Debug.Log("Attack Stopped");
                isAttacking = false;
                anim.SetBool("Attack", isAttacking);
            }
        }
    }
}