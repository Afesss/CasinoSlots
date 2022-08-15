using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private Sound sound;
    [SerializeField] private Music music;
    public override void InstallBindings()
    {
        Container.BindInstance(sound).AsSingle();
        Container.BindInstance(music).AsSingle();
    }
}