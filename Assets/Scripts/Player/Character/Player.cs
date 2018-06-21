using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Image healthBar;
        [SerializeField]
        Text healthText;
        public float currentHealth;
        public float maxHealth = 100;
        [SerializeField]
        Image manaBar;
        [SerializeField]
        Text manaText;
        public float currentMana;
        public float maxMana = 100;
        [SerializeField]
        Image expBar;
        [SerializeField]
        Text expText;
        public float currentExp;
        public float maxExp = 50;
        [SerializeField]
        private Stat mana;
        [SerializeField]
        float startMana = 50;

        public static int currentLevel = 1;
        [SerializeField]
        private Block[] blocks;
        [SerializeField]
        private Transform[] exitPoints;
        [SerializeField]
        LayerMask layerMask;

        private int exitIndex = 2;

        public Transform MyTarget { get; set; }


        protected override void Start()
        {
            base.Start();
            currentHealth = maxHealth;
            currentMana = maxMana;
        }
        protected override void Update()
        {
            GetInput();
            base.Update();

            healthText.text = currentHealth + " / " + maxHealth;
            manaText.text = currentMana + " / " + maxMana;
            expText.text = currentExp + " / " + maxExp;
            if (currentHealth >= 0)
            {
                healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth / maxHealth, Time.deltaTime * 10);
            }
            else
            {
                Debug.Log("Dead");
            }
            if (currentHealth >= 0)
            {
                manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, currentMana / maxMana, Time.deltaTime * 10);
            }
            expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, currentExp / maxExp, Time.deltaTime * 10);
            if (currentExp >= maxExp)
            {
                currentLevel++;
                CharInfo.bonusPoint += 3;
                currentExp -= maxExp;
                maxExp *= 1.5f;
                maxHealth *= 1.2f;
                maxMana *= 1.1f;
            }
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
                currentHealth += 10;
                currentMana += 10;
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                currentHealth -= 10;
                currentMana -= 10;
            }
            #endregion
            foreach (string action in KeybindManager.Instance.ActionBinds.Keys)
            {
                if (Input.GetKeyDown(KeybindManager.Instance.ActionBinds[action]))
                {
                    UIManager.Instance.ClickActionButton(action);
                }
            }
        }
        IEnumerator Attack(string spellName)
        {
            Transform currentTarget = MyTarget;
            Spell newSpell = SpellBook.Instance.CastSpell(spellName);
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
            SpellBook.Instance.StopCasting();
            base.StopAttack();
        }
    }
}