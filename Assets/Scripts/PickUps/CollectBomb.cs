namespace AsteroidBelt.PickUps
{
    public class CollectBomb : CollectMe
    {
        public int bombsRegained;

        public override void CollectThis()
        {
            GainBombs();
        }

        private void GainBombs()
        {
            if (player.bombsAmmo < player.maxBombAmmo)
            {
                player.bombsAmmo += bombsRegained;
            }
            if (player.bombsAmmo > player.maxBombAmmo)
            {
                player.bombsAmmo = player.maxBombAmmo;
            }
        }
    }
}
