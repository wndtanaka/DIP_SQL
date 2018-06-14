using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class Enemy : NPC
    {
        [SerializeField]
        CanvasGroup healthGroup;

        private Transform target;
        public Transform Target
        {
            get
            {
                return target;
            }

            set
            {
                target = value;
            }
        }

        protected override void Update()
        {
            base.Update();
            FollowTarget();
        }
        public override Transform Select()
        {
            healthGroup.alpha = 1;
            return base.Select();
        }
        public override void DeSelect()
        {
            healthGroup.alpha = 0;
            base.DeSelect();
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            OnHealthChanged(health.MyCurrentValue);
        }

        void FollowTarget()
        {
            if (target != null)
            {
                direction = (target.transform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                direction = Vector2.zero;
            }
        }
    }
}