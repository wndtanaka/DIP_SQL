using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class Player : Character
    {
        [SerializeField]
        private Stat health;
        [SerializeField]
        private Stat mana;

        [SerializeField]
        float startHealth = 100;
        [SerializeField]
        float startMana = 50;
        [SerializeField]
        private Block[] blocks;
        [SerializeField]
        private Transform[] exitPoints;
        [SerializeField]
        LayerMask layerMask;

        private int exitIndex = 2;

        private SpellBook spellBook;

        public Transform MyTarget { get; set; }

        protected override void Start()
        {
            base.Start();
            spellBook = GetComponent<SpellBook>();
            health.Initialize(startHealth, startHealth);
            mana.Initialize(startMana, startMana);
        }
        protected override void Update()
        {
            GetInput();
            base.Update();
        }

        void GetInput()
        {
            direction = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                exitIndex = 0;
                direction += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                exitIndex = 2;
                direction += Vector2.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                exitIndex = 3;
                direction += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                exitIndex = 1;
                direction += Vector2.right;
            }
            #region Debugging Only
            if (Input.GetKeyDown(KeyCode.I))
            {
                health.MyCurrentValue += 10;
                mana.MyCurrentValue += 10;
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                health.MyCurrentValue -= 10;
                mana.MyCurrentValue -= 10;
            }
            #endregion
        }
        IEnumerator Attack(int spellIndex)
        {
            Spell newSpell = spellBook.CastSpell(spellIndex);
            isAttacking = true;
            anim.SetBool("Attack", isAttacking);
            yield return new WaitForSeconds(newSpell.CastTime);
            SpellScript s = Instantiate(newSpell.SpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            s.MyTarget = MyTarget;
            StopAttack();
        }

        public void CastSpell(int spellIndex)
        {
            Block();
            if (MyTarget != null && !isAttacking && !IsMoving && InLineOfSight())
            {
                attackRoutine = StartCoroutine(Attack(spellIndex));
            }
        }

        private bool InLineOfSight()
        {
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.position), layerMask);
            if (hit.collider == null)
            {
                return true;
            }
            return false;
        }

        private void Block()
        {
            foreach (Block b in blocks)
            {
                b.Deactivate();
            }
            blocks[exitIndex].Activate();
        }
        public override void StopAttack()
        {
            spellBook.StopCasting();
            base.StopAttack();
        }
    }
}