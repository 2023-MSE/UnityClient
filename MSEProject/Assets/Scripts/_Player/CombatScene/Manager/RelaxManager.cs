using System;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace _Player.CombatScene
{
    public class RelaxManager :  MonoBehaviour
    {
        private Player _player;

        private Boolean check = false;

        private CombatManager _combatManager;
        
        
        // check cool down
        private CoolDown _coolDown;
        private void Start()
        {
          
            Debug.Log("start relax Scene");
            _combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();

            _coolDown = GameObject.Find("CoolDown").GetComponent<CoolDown>();
            
            
            _coolDown.DamageToZero();


        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)&&check)
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.GetComponent<Player>().AnimateIsDrink();
                player.GetComponent<Player>().setHp(0.3f);
                _coolDown.RelaxHp(0.3f);
            }
        }

        public void Scenecheck()
        {
            check = !check;
        }
    }
}