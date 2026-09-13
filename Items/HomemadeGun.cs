using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp1509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ManyCItems.Items
{
    public class HomemadeGun : CustomWeapon
    {
        public override uint Id { get; set; } = 55;
        public override string Name { get; set; } = "Самодельный пистолет.";
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = 1.0f;
        public override Vector3 Scale { get; set; } = new Vector3(0.3f, 0.7f, 0.3f);
        public override ItemType Type { get; set; } = ItemType.GunCOM18;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties();

        public override byte ClipSize { get; set; } = 1;

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += OnHurting;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;

            base.UnsubscribeEvents();
        }

        protected override void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null || ev.Player == null || !Check(ev.Attacker.CurrentItem))
                return;

            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                ev.Player.Health = 100 - ev.DamageHandler.Damage;
            }

            ev.Player.EnableEffect(EffectType.Bleeding);

            base.OnHurting(ev);
        }
    }
}
