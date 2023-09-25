using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player performs a Jump.
    /// </summary>
    /// <typeparam name="PlayerHurt"></typeparam>
    public class PlayerHurt : Simulation.Event<PlayerHurt>
    {
        public PlayerController player;

        public override void Execute()
        {
            if (player.audioSource && player.ouchAudio)
                player.audioSource.PlayOneShot(player.ouchAudio);
        }
    }
}