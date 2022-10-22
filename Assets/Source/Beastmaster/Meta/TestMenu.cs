using System.Collections.Generic;
using Beastmaster.Core.Configs;
using Beastmaster.Core.Primitives;
using Beastmaster.Core.State.Fight;
using Common.Serialization;
using Newtonsoft.Json;
using Riptide;
using Riptide.Transports.Tcp;
using UnityEngine;
using UnityEngine.UI;

namespace Beastmaster.Meta
{
    public class TestMenu : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private Client _client;

        private void Awake()
        {
            _client = new Client(new TcpClient());
            _client.Connect("127.0.0.1:7000");
            _button.onClick.AddListener(StartConnection);
        }

        private void StartConnection()
        {
            var path = new Path();
            path.Add(new Coordinates(){ X = 3, Y = 2});
            path.Add(new Coordinates(){ X = 5, Y = 7});

            var bytes = BinarySerializer.Serialize(new MoveUnitAction.Data(1, 571, path, true));
            
            var msg = Message.Create(MessageSendMode.Reliable, 0);
            msg.AddBytes(bytes);
            _client.Send(msg);
        }

        private void Update()
        {
            _client.Update();
        }

        private void OnDestroy()
        {
            _client.Disconnect();
        }
    }
}