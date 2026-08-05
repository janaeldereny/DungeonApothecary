using UnityEngine;

   [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
    public class EnemySO : ScriptableObject
    {
        public EnemyTypes EnemyType;
        public ItemScriptableObject requiredCure;   

    }
