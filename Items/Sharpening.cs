using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
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
    [CustomItem(ItemType.SCP1509)]
    public class Sharpening : CustomWeapon
    {
        public override uint Id { get; set; } = 54;
        public override string Name { get; set; } = "Заточка";
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = 1.0f;
        public override Vector3 Scale { get; set; } = new Vector3(0.3f, 0.7f, 0.3f);
        public override ItemType Type { get; set; } = ItemType.SCP1509;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties();

        public override float Damage { get; set; } = UnityEngine.Random.Range(5f, 15f);

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Scp1509.Resurrecting += OnResurrectring;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Scp1509.Resurrecting -= OnResurrectring;

            base.UnsubscribeEvents();
        }

        protected void OnResurrectring(ResurrectingEventArgs ev)
        {
            if (ev.Player == null || ev.Player == null || !Check(ev.Player.CurrentItem))
                ev.IsAllowed = false;
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

            ev.Attacker.RemoveItem(ev.Attacker.CurrentItem);

            base.OnHurting(ev);
        }
    }
}
