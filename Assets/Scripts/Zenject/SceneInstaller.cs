using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GameManager gameManager;
    public override void InstallBindings()
    {
        Container.BindInstance(gameManager).AsSingle();
    }
}