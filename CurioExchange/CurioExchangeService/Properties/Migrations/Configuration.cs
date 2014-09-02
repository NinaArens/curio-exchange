namespace CurioExchange.Migrations
{
    using CurioExchange.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CurioExchangeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CurioExchange";
        }

        protected override void Seed(CurioExchangeContext context)
        {
            var none = new CurioExchange.Entities.Collection { Name = "(None)" };
            var astro = new CurioExchange.Entities.Collection { Name = "Astro" };
            var bird = new CurioExchange.Entities.Collection { Name = "Bird" };
            var bone = new CurioExchange.Entities.Collection { Name = "Bone" };
            var face = new CurioExchange.Entities.Collection { Name = "Face" };
            var feather = new CurioExchange.Entities.Collection { Name = "Feather" };
            var figure = new CurioExchange.Entities.Collection { Name = "Figure" };
            var flower = new CurioExchange.Entities.Collection { Name = "Flower" };
            var flutter = new CurioExchange.Entities.Collection { Name = "Flutter" };
            var forsaken = new CurioExchange.Entities.Collection { Name = "Forsaken" };
            var iceAngel = new CurioExchange.Entities.Collection { Name = "Ice Angel" };
            var light = new CurioExchange.Entities.Collection { Name = "Light" };
            var mechanical = new CurioExchange.Entities.Collection { Name = "Mechanical" };
            var scholar = new CurioExchange.Entities.Collection { Name = "Scholar" };
            var sky = new CurioExchange.Entities.Collection { Name = "Sky" };
            var snake = new CurioExchange.Entities.Collection { Name = "Snake" };
            var soulless = new CurioExchange.Entities.Collection { Name = "Soulless" };
            var spider = new CurioExchange.Entities.Collection { Name = "Spider" };
            var tool = new CurioExchange.Entities.Collection { Name = "Tool" };
            var torture = new CurioExchange.Entities.Collection { Name = "Torture" };
            var toy = new CurioExchange.Entities.Collection { Name = "Toy" };
            var utensil = new CurioExchange.Entities.Collection { Name = "Utensil" };
            var vernal = new CurioExchange.Entities.Collection { Name = "Vernal" };
            var waystation = new CurioExchange.Entities.Collection { Name = "Waystation" };

            context.Collections.AddOrUpdate(none);
            context.Collections.AddOrUpdate(astro);
            context.Collections.AddOrUpdate(bird);
            context.Collections.AddOrUpdate(bone);
            context.Collections.AddOrUpdate(face);
            context.Collections.AddOrUpdate(feather);
            context.Collections.AddOrUpdate(figure);
            context.Collections.AddOrUpdate(flower);
            context.Collections.AddOrUpdate(flutter);
            context.Collections.AddOrUpdate(forsaken);
            context.Collections.AddOrUpdate(iceAngel);
            context.Collections.AddOrUpdate(mechanical);
            context.Collections.AddOrUpdate(scholar);
            context.Collections.AddOrUpdate(sky);
            context.Collections.AddOrUpdate(snake);
            context.Collections.AddOrUpdate(soulless);
            context.Collections.AddOrUpdate(spider);
            context.Collections.AddOrUpdate(tool);
            context.Collections.AddOrUpdate(torture);
            context.Collections.AddOrUpdate(toy);
            context.Collections.AddOrUpdate(utensil);
            context.Collections.AddOrUpdate(vernal);
            context.Collections.AddOrUpdate(waystation);

            context.Collections.AddOrUpdate(none);

            var jewelOrchid = new CurioExchange.Entities.Set { Name = "Jewel Orchid", Collection = flower };

            context.Sets.AddOrUpdate(jewelOrchid);

            context.Pieces.AddOrUpdate(new Piece { Name = "First Gem Petal", Rare = false, Set = jewelOrchid });
            context.Pieces.AddOrUpdate(new Piece { Name = "Fluted Stem", Rare = true, Set = jewelOrchid });
            
            context.SaveChanges();
        }
    }
}
