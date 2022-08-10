using Cinemachine;

using Fusion;
using Fusion.Sockets;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : NetworkBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkObject _playerPrefab;
    [SerializeField] private GameObject _playerObjectPointer;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private CinemachineTargetGroup _targetGroup;
    [SerializeField] private int _playerWeight;
    [SerializeField] private float _playerRadius;

    private bool _isStarted = true;
    private MovementControl _playerMovement;
    private SkillsControl _playerSkills;
    private Unit _player;

    [Networked(OnChanged = nameof(OnLoadedPlayersCountChanged))]
    public int LoadedPlayersCount { get; set; }
    public int PlayersCount { get; private set; }
    public MovementControl Movement => _playerMovement;
    public SkillsControl PlayerSkills => _playerSkills;
    public Unit Player => _player;

    public event Action PlayerSpawned;

    public override void Spawned()
    {
        LoadedPlayersCount++;
        Runner.AddCallbacks(this);
        PlayersCount = Runner.ActivePlayers.Count();
    }

    public void CreatePlayer(PointsDistributor pointsDistributor)
    {
        _loadingScreen.SetActive(true);

        var player = Runner.Spawn(_playerPrefab);
        Runner.SetPlayerObject(Runner.LocalPlayer, player);
        _playerMovement = player.GetComponent<MovementControl>();
        _playerSkills = player.GetComponent<SkillsControl>();
        _player = player.GetComponent<Unit>();
        Instantiate(_playerObjectPointer, _player.transform);

        _playerMovement.enabled = false;
        _playerSkills.enabled = false;
        var index = Mathf.Clamp(Runner.LocalPlayer, 0, _spawnPoints.Length);
        _player.transform.position = _spawnPoints[index].position;

        var health = pointsDistributor.Values[StatType.Health].CurrentValue;
        var mana = pointsDistributor.Values[StatType.Mana].CurrentValue;
        var critChance = pointsDistributor.Values[StatType.CritChance].CurrentValue;
        var evadeChance = pointsDistributor.Values[StatType.EvadeChance].CurrentValue;
        _player.RPC_AddHealth(health, Addition.MaxAndCurrent);
        _player.RPC_AddMana(mana, Addition.MaxAndCurrent);
        _player.CritChance += critChance;
        _player.EvadeChance += evadeChance;

        PlayerSpawned?.Invoke();
        _isStarted = false;
    }

    private static void OnLoadedPlayersCountChanged(Changed<Game> changed)
    {
        changed.LoadNew();
        var behaviour = changed.Behaviour;

        if (behaviour.LoadedPlayersCount == behaviour.PlayersCount)
        {
            behaviour.RPC_StartPointDistribution();
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (_isStarted == false)
        {
            _isStarted = Runner
                .ActivePlayers
                .All(x => Runner.GetPlayerObject(x) != null);

            if (_isStarted)
                StartGame();
        }
    }

    private void StartGame()
    {
        foreach (var player in Runner.ActivePlayers)
        {
            var playerObject = Runner.GetPlayerObject(player);
            playerObject.GetComponent<Unit>().Died += () => OnPlayerDied(playerObject);
            _targetGroup.AddMember(playerObject.transform, _playerWeight, _playerRadius);
        }

        _playerMovement.enabled = true;
        _playerSkills.enabled = true;

        _loadingScreen.SetActive(false);
    }

    [Rpc]
    private void RPC_StartPointDistribution()
    {
        _loadingScreen.SetActive(false);
    }

    private void OnPlayerDied(NetworkObject player)
    {
        player.GetComponent<Animator>().SetBool("died", true);
        player.GetComponent<MovementControl>().enabled = false;
        player.GetComponent<SkillsControl>().enabled = false;
        _targetGroup.RemoveMember(player.transform);

        _gameOverScreen.SetActive(true);
    }

    public async void Leave()
    {
        _loadingScreen.SetActive(true);
        await Runner.Shutdown();
        SceneManager.LoadScene(0);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player left");
        Leave();
    }

    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
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
