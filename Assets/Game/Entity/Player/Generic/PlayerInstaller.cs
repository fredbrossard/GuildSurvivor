using Game.Entity.Player;
using Game.Settings;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [Inject] GameSettingsInstaller.PlayerSettings m_playerSettings;
    [SerializeField] private PlayerFactory m_factory;

    public override void InstallBindings()
    {
        m_factory.Bind(Container);
        Container.BindInstance(m_factory);
        Container.BindFactory<PlayerModel, PlayerObj, PlayerObj.Factory>().FromFactory<PlayerFactory>();
        //if (m_playerSettings != null && m_playerSettings.models != null)
        //{
        //    foreach (var model in m_playerSettings.models)
        //    {
        //    }
        //}
    }

}
