using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using UnityEngine;

namespace Beastmaster.Network
{
    public class NetworkManager
    {
        private readonly ConnectionInfo _connectionInfo = new ConnectionInfo("192.168.1.67", 57636);

        private Connection _connection;
        private readonly NetworkState _networkState;

        public NetworkManager(NetworkState networkState)
        {
            _networkState = networkState;
        }
        
        public void Connect()
        {
            if (_connection != null)
                return;
            
            _connection = TCPConnection.GetConnection(_connectionInfo);
            if (_connection.ConnectionAlive())
            {
                Debug.LogError("Connected to server");
                _networkState.Connected = true;
            }
            _connection.AppendShutdownHandler(HandleConnectionShutdown);
        }

        private void HandleConnectionShutdown(Connection connection)
        {
            Debug.LogError("Connection to server is lost");
        }
    }
}