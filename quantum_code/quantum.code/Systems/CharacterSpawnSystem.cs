using System;
using Quantum.Collections;

namespace Quantum.Systems;


public unsafe class CharacterSpawnSystem : SystemSignalsOnly, ISignalOnPlayerDataSet
{
    
    private void SetPlayerPosition(Frame f, EntityRef player)
    {
        var spawnPoints = f.ResolveList(f.Global->AvailableSpawnPoints);
        var number = f.RNG->Next(0, spawnPoints.Count);
        f.Unsafe.GetPointer<Transform3D>(player)->Position = f.Get<Transform3D>(spawnPoints[number]).Position;
    }

    
    private void CreateSpawnPointList(Frame f){
        
        if(f.TryResolveList(f.Global->AvailableSpawnPoints, out _))
            return;
        
        f.Global->AvailableSpawnPoints = f.AllocateList<EntityRef>();
        QList<EntityRef> spawnPoints = f.ResolveList(f.Global->AvailableSpawnPoints);
        foreach (var (entity, _) in f.Unsafe.GetComponentBlockIterator<SpawnPoint>())
        {
            spawnPoints.Add(entity);
        }
    }


    public void OnPlayerDataSet(Frame f, PlayerRef playerRef)
    {
        EntityPrototype prototypeAsset = f.FindAsset<EntityPrototype>(f.SimulationConfig.PlayerPrefab.Id);
        EntityRef player = f.Create(prototypeAsset);
        Player* playerID = f.Unsafe.GetPointer<Player>(player);
        playerID->player = playerRef;
        
        CreateSpawnPointList(f);
        SetPlayerPosition(f, player);
        f.Events.OnPlayerSpawned(player, playerRef);
    }
}