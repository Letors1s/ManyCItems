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
    public class Knife : CustomWeapon
    {
        public override uint Id { get; set; } = 52;
        public override string Name { get; set; } = "Нож";
        public override string Description { get; set; } = "<color=gray>Обычный нож, довольно хорошо заточен.</color>";
        public override float Weight { get; set; } = 1.0f;
        public override Vector3 Scale { get; set; } = new Vector3(0.9f, 1.2f, 0.9f);
        public override ItemType Type { get; set; } = ItemType.SCP1509;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            Limit = 5,

            StaticSpawnPoints = new List<StaticSpawnPoint>
            {
                new StaticSpawnPoint
                {
                    Position = new Vector3(),
                    Chance = 15,
                    Name = "PC15_knifeSpawn"
                }
            }
        };

        public override float Damage { get; set; } = 10f;

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

            if(UnityEngine.Random.Range(1, 6) == 1)
            {
                ev.Player.ShowHint("Вы чувствуете оглушение.");
                ev.Player.EnableEffect(EffectType.Blurred, 5f);
            }

            if (UnityEngine.Random.Range(1, 6) == 1)
            {
                ev.Player.ShowHint("Рана вызвала у вас кровотечение.");
                ev.Player.EnableEffect(EffectType.Bleeding);
            }

            base.OnHurting(ev);
        }
    }
}
