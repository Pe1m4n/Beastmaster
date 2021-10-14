using Beastmaster.Core.Configs;
using Beastmaster.Core.Controllers;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using Beastmaster.Core.View;
using Beastmaster.Core.View.Configs;
using Beastmaster.Core.View.Units;
using Common.PathFinding;
using UnityEngine;
using Zenject;

namespace Beastmaster.Core.Installers
{
    public class FightSceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private TileGridCreationStrategy _tileGridCreationStrategy;
        [SerializeField] private FightConfig _fightConfig;
        [SerializeField] private UnitPrefabProvider _unitPrefabProvider;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameLoop>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LocalStateActionMediator>().AsSingle();
            Container.Bind<PlayerController>().AsSingle().WithArguments(_fightConfig);
            Container.Bind<ActionsBindingContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<FightInputContainer>().AsSingle().WithArguments(_camera);
            Container.Bind<FightStateContainer>().AsSingle().WithArguments(_fightConfig);
            
            BindViews(); 
        }

        private void BindViews()
        {
            Container.Bind<ViewsContainer>().AsSingle();
            Container.Bind<TilesView>().AsSingle().WithArguments(_tileGridCreationStrategy, transform);
            Container.Bind<UnitsView>().AsSingle().WithArguments(transform);

            Container.BindInterfacesTo<SimpleUnitFactory>().AsSingle().WithArguments(_unitPrefabProvider);

            Container.Bind<ViewActionsBindingContainer>().AsSingle();
            Container.Bind<ViewActionsQueue>().AsSingle();
            BindViewActions();
        }

        private void BindViewActions()
        {
            Container.Bind<MoveUnitViewAction>().AsSingle();
        }
    }
}