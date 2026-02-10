using Game.Entity.Player;
using UnityEngine;
using Zenject;

public class PlayerFactory : MonoBehaviour, IFactory<PlayerModel,PlayerObj>
{
    private DiContainer m_diContainer { get; set; }

    public void Bind(DiContainer _container)
    {
        m_diContainer = _container;
    }

    public PlayerObj Create(PlayerModel _model)
    {
        PlayerObj obj = m_diContainer.InstantiatePrefab(_model.prefab).GetComponent<PlayerObj>();
        obj.Bind(_model);
        return obj.GetComponent<PlayerObj>(); 
    }
}
