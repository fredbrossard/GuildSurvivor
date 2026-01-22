using Game.Entity.Generic;
using System;
using UnityEngine;

namespace Game.Entity.Player
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Model/PlayerModel", order = 1), Serializable]
    public class PlayerModel : ScriptableObject, IEntityModel
    {
        public Sprite sprite;

        public uint initialMaxHealth;
        public byte initialLvl;
        public uint initialAttack;
        public float cooldownAttack;
        public float initialSpeed;
    }
}