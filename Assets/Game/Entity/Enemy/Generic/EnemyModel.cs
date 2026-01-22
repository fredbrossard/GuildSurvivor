using Game.Entity.Generic;
using System;
using UnityEngine;

namespace Game.Entity.Model
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Model/EnemyModel", order = 2), Serializable]
    public class EnemyModel : ScriptableObject, IEntityModel
	{
        public Sprite sprite;

        public uint initialMaxHealth;
        public byte initialLvl;
        public uint initialAttack;
        public float cooldownAttack;
    }
}