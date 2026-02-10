using Game.Entity.Player;
using System;
using UnityEngine;
using Zenject;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [Header("Player")]
        [SerializeField] PlayerSettings playerSettings;
        [SerializeField] EnemySettings enemySettings;

        [Serializable]
        public class PlayerSettings
        {
            public uint maxHealth;
            public float maxSpeed;

            public PlayerModel[] models;
        }

        [Serializable]
        public class EnemySettings
        {
            public uint maxHealth;
            public ushort maxSpeed;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(playerSettings);
            Container.BindInstance(enemySettings);
        }
    }
}