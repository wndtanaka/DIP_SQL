using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class Player : Character
    {
        private static Player instance;

        public static Player MyInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Player>();
                }

                return instance;
            }
        }

        [SerializeField]
        private Stat mana;

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
            if (Input.GetKey(KeybindManager.Instance.Keybinds["Up"]))
            {
                exitIndex = 0;
                direction += Vector2.up;
            }
            if (Input.GetKey(KeybindManager.Instance.Keybinds["Down"]))
            {
                exitIndex = 2;
                direction += Vector2.down;
            }
            if (Input.GetKey(KeybindManager.Instance.Keybinds["Left"]))
            {
                exitIndex = 3;
                direction += Vector2.left;
            }
            if (Input.GetKey(KeybindManager.Instance.Keybinds["Right"]))
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
            foreach (string action in KeybindManager.Instance.ActionBinds.Keys)
            {
                if (Input.GetKey(KeybindManager.Instance.ActionBinds[action]))
                {
                    UIManager.Instance.ClickActionButton(action);
                }
            }
        }
        IEnumerator Attack(string spellName)
        {
            Transform currentTarget = MyTarget;
            Spell newSpell = spellBook.CastSpell(spellName);
            isAttacking = true;
            anim.SetBool("Attack", isAttacking);
            yield return new WaitForSeconds(newSpell.CastTime);
            if (currentTarget != null && InLineOfSight())
            {
                SpellScript s = Instantiate(newSpell.SpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
                s.Initialized(currentTarget, newSpell.Damage);
            }
            StopAttack();
        }

        public void CastSpell(string spellName)
        {
            Block();
            if (MyTarget != null && !isAttacking && !IsMoving && InLineOfSight())
            {
                attackRoutine = StartCoroutine(Attack(spellName));
            }
        }

        private bool InLineOfSight()
        {
            if (MyTarget != null)
            {
                Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.position), layerMask);
                if (hit.collider == null)
                {
                    return true;
                }
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