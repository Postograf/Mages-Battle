using Fusion;
using Fusion.Sockets;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class MainMenu : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private GameObject _waitScreen;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private NetworkRunner _runner;
        [SerializeField] private string _gameScene;
        [SerializeField] private int _playerCount;

        private NetworkSceneManagerDefault _sceneManager;

        private void Awake()
        {
            _runner.AddCallbacks(this);

            _waitScreen.SetActive(false);
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _cancelButton.onClick.AddListener(OnCancelButtonClicked);

            if (_runner.TryGetComponent(out _sceneManager) == false)
            {
                _sceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            }
        }

        public async void OnPlayButtonClicked()
        {
            _waitScreen.SetActive(true);
            _cancelButton.interactable = false;

            await _runner.StartGame(new StartGameArgs()
            {
                GameMode = GameMode.Shared,
                PlayerCount = _playerCount,
                SceneManager = _sceneManager
            });

            _cancelButton.interactable = true;
        }


        public async void OnCancelButtonClicked()
        {
            _waitScreen.SetActive(false);
            _playButton.interactable = false;
            await _runner.Shutdown(false);
            _playButton.interactable = true;
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log("Joined");
            if (_runner.ActivePlayers.Count() == _playerCount)
            {
                Debug.Log("All joined");
                _runner.SessionInfo.IsOpen = false;
                _runner.SetActiveScene(_gameScene);
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnInput(NetworkRunner runner, NetworkInput input) { }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
        public void OnConnectedToServer(NetworkRunner runner) { Debug.Log("Connected"); }
        public void OnDisconnectedFromServer(NetworkRunner runner) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }
    }
}