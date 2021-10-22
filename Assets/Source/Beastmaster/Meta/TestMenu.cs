using System;
using Beastmaster.Network;
using NetworkCommsDotNet;
using UnityEngine;
using UnityEngine.UI;

namespace Beastmaster.Meta
{
    public class TestMenu : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private readonly NetworkState _networkState = new NetworkState();
        private NetworkManager _networkManager;
        private void Awake()
        {
            _networkManager = new NetworkManager(_networkState);
            _button.onClick.AddListener(_networkManager.Connect);
        }

        private void OnDestroy()
        {
        }
    }
}