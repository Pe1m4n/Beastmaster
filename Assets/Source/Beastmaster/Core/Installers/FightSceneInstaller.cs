using Beastmaster.Core.Configs;
using Beastmaster.Core.Controllers;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using Beastmaster.Core.Unity.State;
using Beastmaster.Core.View;
using Beastmaster.Core.View.Configs;
using Beastmaster.Core.View.UI;
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
        [SerializeField] private TestFightConfigFactory _testFightConfigFactory; //TODO: pass from outside
        [SerializeField] private UnitPrefabProvider _unitPrefabProvider;

        [Header("UI")] 
        [SerializeField] private UIView _uiView;
        
        [InjectOptional] private bool _remoteStateHandling;
            
        public override void InstallBindings()
        {
            var fightConfig = _testFightConfigFactory.Create();
            
            Container.BindInterfacesTo<GameLoop>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LocalStateActionMediator>().AsSingle();
            Container.Bind<PlayerController>().AsSingle();
            Container.Bind<ActionsBindingContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<FightInputContainer>().AsSingle().WithArguments(_camera);
            Container.Bind<FightStateContainer>().AsSingle().WithArguments(fightConfig);
            
            BindStateHandling();
            BindViews();
        }

        private void BindStateHandling()
        {
            if (!_remoteStateHandling)
            {
                Container.Bind<LocalFightStateHandler>().AsSingle();
                Container.BindInterfacesTo<LocalStateHandlerTicker>().AsSingle();
            }
        }

        private void BindViews()
        {
            Container.Bind<ViewsContainer>().AsSingle();
            Container.Bind<TilesView>().AsSingle().WithArguments(_tileGridCreationStrategy, transform);
            Container.Bind<UnitsView>().AsSingle().WithArguments(transform);
            Container.Bind<UIView>().FromInstance(_uiView).AsSingle();

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