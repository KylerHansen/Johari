using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //USED FOR SEEDING DUMMY DATA IN DATABASE. 

            context.Database.EnsureCreated();

            if (context.Adjective.Any()) //look for any categories.
            {
                return; //DB has been seeded
            }

            var adjective = new List<Adjective>
            {
                new Adjective{Name="Able", Definition="Having the power, skill, means, or opportunity to do something", Type=1},
                new Adjective{Name="Accepting", Definition="tending to regard different types of people and ways of life with tolerance and acceptance", Type=1},
                new Adjective{Name="bold", Definition="showing or requiring a fearless daring spirit", Type=1},
                new Adjective{Name="brave", Definition="having or showing mental or moral strength to face danger, fear, or difficulty", Type=1},
                new Adjective{Name="calm", Definition="a quiet and peaceful state or condition", Type=1},
                new Adjective{Name="caring", Definition="feeling or showing concern for or kindness to others", Type=1},
                new Adjective{Name="clever", Definition="showing intelligent thinking", Type=1},
                new Adjective{Name="dependable", Definition="capable of being trusted or depended on", Type=1},
                new Adjective{Name="dignified", Definition="showing or expressing dignity (the quality or state of being worthy, honored, or esteemed)", Type=1},
                new Adjective{Name="helpful", Definition="of service or assistance", Type=1},
                new Adjective{Name="irrational", Definition="not based on, or not using, clear logical thought", Type=0},
                new Adjective{Name="imperceptive", Definition="lacking in perception or insight", Type=0},
                new Adjective{Name="loud", Definition="talking very loudly, too much and in a way that is annoying", Type=0},
                new Adjective{Name="self-satisfied", Definition="too pleased with yourself or your own achievements", Type=0},
                new Adjective{Name="overdramatic", Definition="excessively dramatic or exaggerated", Type=0},
                new Adjective{Name="unreliable", Definition="that cannot be trusted or depended on", Type=0},
                new Adjective{Name="inflexible", Definition="unwilling to change their opinions, decisions, etc., or the way they do things", Type=0},
                new Adjective{Name="glum", Definition="sad, quiet and unhappy", Type=0},
                new Adjective{Name="vulgar", Definition="not having or showing good taste; not polite, pleasant or well behaved", Type=0},
                new Adjective{Name="unhappy", Definition="not pleased or satisfied with somebody/something", Type=0},
            };

            foreach (Adjective a in adjective)
            {
                context.Adjective.Add(a);
            }
            context.SaveChanges();

            //var foodtype = new List<FoodType>
            //{
            //    new FoodType{Name="Beef"},
            //    new FoodType{Name="Chicken"},
            //    new FoodType{Name="Veggie"},
            //    new FoodType{Name="Sugar Free"},
            //    new FoodType{Name="SeaFood"},
            //    new FoodType{Name="Dairy Free"}
            //};

            //foreach (FoodType f in foodtype)
            //{
            //    context.FoodType.Add(f);
            //}

            //context.SaveChanges();
           
        }
    }
}
