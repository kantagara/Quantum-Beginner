using Quantum.Core;
using Quantum.Systems;

namespace Quantum;

public static class SystemSetup
{
    public static SystemBase[] CreateSystems(RuntimeConfig gameConfig, SimulationConfig simulationConfig)
    {
        return new[]
        {
            // pre-defined core systems
            new CullingSystem2D(),
            new CullingSystem3D(),

            new PhysicsSystem2D(),
            new PhysicsSystem3D(),

            DebugCommand.CreateSystem(),

            new NavigationSystem(),
            new EntityPrototypeSystem(),
            new PlayerConnectedSystem(),
            new CharacterSpawnSystem(),
            new CharacterMovementSystem()

            // user systems go here 
        };
    }
}