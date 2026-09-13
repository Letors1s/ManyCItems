using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp1509;
using System.Collections.Generic;
using UnityEngine;

namespace ManyCItems.Items
{
    [CustomItem(ItemType.SCP1509)]
    public class KitchenKnife : CustomWeapon
    {
        public override uint Id { get; set; } = 53;
        public override string Name { get; set; } = "Кухонный нож";
        public override string Description { get; set; } = "<color=gray>Обычный кухонный нож.</color>";
        public override float Weight { get; set; } = 1.0f;
        public override Vector3 Scale { get; set; } = new Vector3(0.9f, 1.35f, 0.9f);
        public override ItemType Type { get; set; } = ItemType.SCP1509;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties();

        public override float Damage { get; set; } = 6f;

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

            if (UnityEngine.Random.Range(1, 9) == 1)
            {
                ev.Player.EnableEffect(EffectType.Bleeding);
            }

            base.OnHurting(ev);
        }
    }
}
