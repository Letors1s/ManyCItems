using Exiled.API.Features;
using Exiled.CustomItems.API;
using ManyCItems.Items;
using System;

namespace ManyCItems
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "ManyCItems";
        public override string Author => "Letors1s";
        public override Version Version => new Version(1, 0, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 14, 2);

        public static Plugin Instance;

        public Knife Knife { get; private set; }
        public KitchenKnife KitchenKnife { get; private set; }
        public Sharpening Sharpening {  get; private set; }
        public HomemadeGun HomemadeGun { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;

            Knife = new Knife();
            KitchenKnife = new KitchenKnife();
            Sharpening = new Sharpening();
            HomemadeGun = new HomemadeGun();

            Knife.Register();
            KitchenKnife.Register();
            Sharpening.Register();
            HomemadeGun.Register();


            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Knife.Unregister();
            KitchenKnife.Unregister();
            Sharpening.Unregister();
            HomemadeGun.Unregister();

            Instance = null;
            base.OnDisabled();
        }
    }
}
