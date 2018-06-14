using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpellScript : MonoBehaviour
    {
        Rigidbody2D rigid;

        [SerializeField]
        float speed = 10;
        int damage;

        public Transform MyTarget { get; private set; }

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Initialized(Transform target, int damage)
        {
            this.MyTarget = target;
            this.damage = damage;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            if (MyTarget != null)
            {
            Vector2 direction = MyTarget.position - transform.position;
            rigid.velocity = direction.normalized * speed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "HitBox" && collision.transform == MyTarget)
            {
                speed = 0;
                collision.GetComponentInParent<Enemy>().TakeDamage(damage);
                GetComponent<Animator>().SetTrigger("Impact");
                rigid.velocity = Vector2.zero;
                MyTarget = null;
            }
        }
    }
}