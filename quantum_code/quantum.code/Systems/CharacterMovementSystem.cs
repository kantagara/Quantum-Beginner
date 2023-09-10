using Photon.Deterministic;

namespace Quantum.Systems;


public unsafe class CharacterMovementSystem : SystemMainThreadFilter<CharacterMovementSystem.Filter>, ISignalOnPlayerConnected
{
    public struct Filter
    {
        public CharacterController3D* CharacterController;
        public Player* Player;
        public Transform3D* Transform;
        public EntityRef Entity;
    }
    
    public override void Update(Frame f, ref Filter filter)
    {
        var player = filter.Player->player;
        var input = f.GetPlayerInput(player);
        var characterController = filter.CharacterController;

        
        filter.Transform->Rotation = FPQuaternion.Lerp(filter.Transform->Rotation,
            FPQuaternion.Euler(FP._0, input->Look, FP._0), FP._0_10); 
        
        FPVector3 targetDirection = FPQuaternion.Euler(FP._0, input->Look, FP._0) * (input->Move == FPVector2.Zero ? FPVector3.Zero : FPVector3.Forward);
        
        
        characterController->Move(f, filter.Entity, targetDirection);
    }

    public void OnPlayerConnected(Frame f, PlayerRef player)
    {
    }
}