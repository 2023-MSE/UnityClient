using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Player.CombatScene
{
    public class Player : Character
    {
        private List<bool> skillAvailability;
        private Animator animator;
        public override void AnimateHitMotion()
        {
            animator.SetTrigger("isDefence");
            animator.SetTrigger("isGetDamage");
        }

        public void AnimateDefenceMotion()
        {
            animator.SetTrigger("isDefence");
        }
        public override void dead()
        {
            /*TODO*/
            Debug.Log("Player dead");
            animator.SetTrigger("isDie");
        }

        public void AnimateSkillMotion()
        {
            animator.SetTrigger("activeSkill");
        }
        public void AnimateIdle(float speed)
        {
            animator = this.GetComponent<Animator>();
            animator.SetFloat("speed", speed);
            Debug.Log("Animator is " + animator);
            Debug.Assert(animator != null, "Animator is NULL");

            int idleType = -1;
            switch (DungeonManager.instance.GetCurrentStageType())
            {
                case DungeonInfoFolder.Stage.StageType.Monster:
                case DungeonInfoFolder.Stage.StageType.Boss:
                    Debug.Log("Set Idle Type : 0");
                    idleType = 0;
                    break;
                case DungeonInfoFolder.Stage.StageType.Relax:
                    Debug.Log("Set Idle Type : 1");
                    idleType = 1;
                    break;
                case DungeonInfoFolder.Stage.StageType.Totem:
                    Debug.Log("Set Idle Type : 2");
                    idleType = 2;
                    break;
            }

            animator.SetInteger("idleType", idleType);
        }

        public void AnimateMissMotion()
        {
            animator.SetTrigger("isAttack");
            animator.SetTrigger("isMiss");
        }
        public void AnimateDirectionMotion(Direction direction)
        {
            animator.SetTrigger("isAttack");
            switch (direction)
            {
                case Direction.UP:
                    animator.SetInteger("dir", 0);
                    break;
                case Direction.DOWN:
                    animator.SetInteger("dir", 1);
                    break;
                case Direction.LEFT:
                    animator.SetInteger("dir", 2);
                    break;
                case Direction.RIGHT:
                    animator.SetInteger("dir", 3);
                    break;
            }
        }

        public void AnimateWinMotion(Direction direction)
        {
            animator.SetTrigger("isWin");
        }

        public void AnimateGoNextMotion(Direction direction)
        {
            animator.SetTrigger("selectMap");
        }
        public void ResetParameter()
        {
            animator.SetInteger("idleType", -1);
            animator.SetInteger("dir", -1);
        }
    }
}