using System;
using Quantum;

public class PlayerEntityView : EntityView
{
    public static Action<bool, CameraController> OnPlayerInstantiated;
    
    public void ViewCreated()
    {
        QuantumGame game = QuantumRunner.Default.Game;
        Frame frame = game.Frames.Predicted;
        
        if (frame.TryGet<Player>(EntityRef, out var player) == false)
            return;
        
        OnPlayerInstantiated?.Invoke(game.PlayerIsLocal(player.player), GetComponent<CameraController>());
    } 
}
