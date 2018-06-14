using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        float startHealth = 100;
        [SerializeField]
        protected float speed;

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
        [SerializeField]
        protected Transform hitBox;
        [SerializeField]
        protected Stat health;
        public Stat Health
        { get
            {
                return health;
            }
        }

        // Use this for initialization
        protected virtual void Start()

        {
            health.Initialize(startHealth, startHealth);
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
                isAttacking = false;
                anim.SetBool("Attack", isAttacking);
            }
        }
        public virtual void TakeDamage(float damage)
        {
            health.MyCurrentValue -= damage;
            if (health.MyCurrentValue <= 0)
            {
                // DIE
                anim.SetTrigger("Die");
            }
        }
    }
}