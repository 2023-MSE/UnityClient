using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Player.CombatScene
{
    public class Player : Character
    {
        private List<bool> skillAvailability;
        public override void hitMotion()
        {
            /*TODO*/
        }
        public override void dead()
        {
            /*TODO*/
            Debug.Log("Player dead");
            this.GetComponent<GameObject>().SetActive(false);
        }

        public void skillMotion()
        {
<<<<<<< Updated upstream
=======
            animator.SetTrigger("activeSkill");
        }

        public void AnimateIsDrink()
        {
            animator.SetTrigger("isDrink");
   
        }
        
        public void AnimateIdle(float speed)
        {
            animator = this.GetComponent<Animator>();
            animator.SetFloat("speed", speed);
            Debug.Log("Animator is " + animator);
            Debug.Assert(animator != null, "Animator is NULL");
>>>>>>> Stashed changes

        }

        public void directionMotion(Direction direction)
        {
            /*TODO*/
        }
    }
}