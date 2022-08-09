using Fusion;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class MainMenu : MonoBehaviour, IPlayerJoined
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

        public void PlayerJoined(PlayerRef player)
        {
            if (_runner.ActivePlayers.Count() == _playerCount)
            {
                _runner.SessionInfo.IsOpen = false;
                _runner.SetActiveScene(_gameScene);
            }
        }
    }
}