using System;
using UnityEngine;
using Zenject;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
       
        public PlayerSettings player;
        public EnemySettings enemy;


        [Serializable]
        public class PlayerSettings
        {
            public uint maxHealth;
            public ushort maxSpeed;
        }

        [Serializable]
        public class EnemySettings
        {
            public uint maxHealth;
            public ushort maxSpeed;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(player);
            Container.BindInstance(enemy);
        }
    }
}