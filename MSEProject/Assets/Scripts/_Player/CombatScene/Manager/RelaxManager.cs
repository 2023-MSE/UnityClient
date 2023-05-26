using System;
using System.Xml.Schema;
using UnityEngine;
using Random = UnityEngine.Random;
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

        private GameObject player;

        [SerializeField] private GameObject card;
        [SerializeField] private GameObject note;

        [SerializeField] private GameObject heal;
        [SerializeField] private GameObject dead;
        // check cool down
        private CoolDown _coolDown;

        private CameraShake _shake;
        
        public float minDamage = 100; // 최소 데미지
        public float maxDamage = 200; // 최대 데미지
        public float minHeal = 50; // 최소 회복량
        public float maxHeal = 100; // 최대 회복량
        private void Start()
        {
            card.SetActive(false);
            
            heal.SetActive(false);
            dead.SetActive(false);
            
            player = GameObject.FindWithTag("Player");
            Debug.Log("start relax Scene");
            _combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
            _shake = FindObjectOfType<CameraShake>();




        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)&&check)
            {
                card.SetActive(true);
             
                //test
                ApplyRandomEffect();


            }
        }

        public void DrinkToTem(CoolDown cooldown, float hp)
        {
            cooldown.RelaxHp(hp*0.001f);
            player.GetComponent<Player>().setHp(+300f);
         
        }

        public void Scenecheck()
        {
            check = !check;
            player = GameObject.FindWithTag("Player");
            _coolDown = GameObject.Find("CoolDown").GetComponent<CoolDown>();
            if (player != null)
            {
                Debug.Log("relax not null"+ null);
            }
            if (_coolDown != null)
            {
                Debug.Log("relax not cooldown"+ null);
            }
            //note.SetActive(false);

          
            
        }
        
        public void ApplyRandomEffect()
        {
            bool shouldDamage = Random.Range(0, 2) == 0; // 0 또는 1 중에서 랜덤으로 선택
            
            Debug.Log("relax : " +  shouldDamage);
            if (shouldDamage)
            {
                //
                dead.SetActive(true);
                heal.SetActive(false);
                player.GetComponent<Player>().AnimateHitMotion();
                float damageAmount = Random.Range(minDamage, maxDamage + 1);
                Debug.Log("relax damage : " +  damageAmount);
                player.GetComponent<Player>().setHp(-damageAmount);
                _coolDown.setHp(player.GetComponent<Player>().getHp()*0.001f);
                _shake.ShakeCamera();

               
            }
            else
            {
                //
                heal.SetActive(true);
                dead.SetActive(false);
                player.GetComponent<Player>().AnimateIsDrink();
                float healAmount = Random.Range(minHeal, maxHeal + 1);
                Debug.Log("relax heal: " +  healAmount);
                player.GetComponent<Player>().setHp(healAmount);
                _coolDown.setHp(player.GetComponent<Player>().getHp()*0.001f);
            }
        }
      
    }
}