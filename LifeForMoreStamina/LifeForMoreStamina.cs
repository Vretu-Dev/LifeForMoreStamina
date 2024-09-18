using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace LifeForMoreStamina
{
    public class LifeForMoreStamina : Plugin<Config>
    {
        private Timer staminaCheckTimer;
        public override string Name => "LifeForMoreStamina";
        public override string Author => "Vretu";
        public override string Prefix { get; } = "LifeForMoreStamina";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(8, 9, 8);

        public override void OnEnabled()
        {
            staminaCheckTimer = new Timer(100);
            staminaCheckTimer.Elapsed += OnStaminaCheck;
            staminaCheckTimer.Start();

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            if (staminaCheckTimer != null)
            {
                staminaCheckTimer.Stop();
                staminaCheckTimer.Dispose();
            }

            base.OnDisabled();
        }
        private void OnStaminaCheck(object sender, ElapsedEventArgs e)
        {
            foreach (var player in Player.List)
            {
                if (player.Role.Side != Side.Scp)
                {
                    if (player.Stamina <= Config.StaminaThreshold)
                    {
                        player.Stamina = Math.Max(player.Stamina, Config.StaminaAdded);

                        if (player.Stamina == Config.StaminaAdded)
                        {
                            player.Health -= Config.HpRemoved;
                        }
                    }
                }
            }
        }
    }
}
