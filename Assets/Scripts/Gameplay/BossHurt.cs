using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the health component on an enemy has a hitpoint value of  0.
    /// </summary>
    /// <typeparam name="BossHurt"></typeparam>
    public class BossHurt : Simulation.Event<BossHurt>
    {
        public EnemyController enemy;

        public override void Execute()
        {
            if (enemy._audio && enemy.damageAudio)
                enemy._audio.PlayOneShot(enemy.damageAudio);
        }
    }
}