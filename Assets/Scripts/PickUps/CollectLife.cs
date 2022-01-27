using AsteroidBelt.Interfaces;

namespace AsteroidBelt.PickUps
{
    public class CollectLife : CollectMe
    {
        public int healthRegained;

        public override void CollectThis()
        {
            GainHealth();
        }

        private void GainHealth()
        {
            if ((player.life + healthRegained) <= player.maxLife)
            {
                player.life += healthRegained;
            }
            else
            {
                player.life = player.maxLife;
            }
            Main.Instance.UpdateLifeHUD(player.life);
        }
    }
}
