using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EverGardenNew.Models;

namespace EverGardenNew.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Plants.Any())
            {
                return;   // DB has been seeded
            }

            var plants = new Plant[]
            {
            new Plant{Name="Aloe", ScientificName="Aloe", CategoryEdibleID=2, CategoryPlaceID=2, Climate="Warm and Dry", Watering="Once a day",
                ShortDescription="The short stem (or trunk) is planted with thick fleshy xiphoid leaves, collected in dense rosettes and located in them in a spiral. ",
                BioDescription="The genus Aloe combines perennial leafy, herbaceous, shrub or tree-like xerophytes and succulents. Short stem (or trunk) with shrunken fleshy xiphoid thick leaves, collected in dense rosettes and located in them in a spiral.",
                SpreadingArea="Plants of the genus Aloe come from the arid regions of South and tropical Africa, Madagascar and the Arabian Peninsula. Aloe mostly grows in warm, dry climates.",
                SmallImage="https://www.almanac.com/sites/default/files/styles/primary_image_in_article/public/image_nodes/aloe-vera-white-pot_sunwand24-ss_edit.jpg?itok=Y7HnaYk3",
                LargeImage="https://www.supersadovnik.ru/binfiles/images/20201215/b4f1385b.jpg",
                Tools="Pot, spatula, spray bottle"},
            new Plant{Name="Tomatoes", ScientificName="Solanum lycopersicum", CategoryEdibleID=1, CategoryPlaceID=1, Climate="Tropical. Warm and wet", Watering="3 times a day",
                ShortDescription="The tomato is the edible berry of the plant Solanum lycopersicum, commonly known as a tomato plant.",
                BioDescription="The tomato has a highly developed rod-type root system. The roots are branched, grow and form quickly. They go into the ground to a great depth (with a seedless culture up to 1 m or more), spreading 1.5-2.5 m in diameter.",
                SpreadingArea="The species originated in western South America and Central America.",
                SmallImage="https://gardenerspath.com/wp-content/uploads/2020/06/How-to-Grow-Tomatoes-Cover.jpg",
                LargeImage="https://tv.ua/i/95/13/35/951335/c9d3c5886a33aaf06f736e662b4111ab-quality_70Xresize_crop_1Xallow_enlarge_0Xw_750Xh_463.jpg",
                Tools="Shovel, scapula, support beams, ropes"},
            };
            foreach (Plant p in plants)
            {
                context.Plants.Add(p);
            }
            context.SaveChanges();

            var plantActivities = new PlantActivity[]
            {
            new PlantActivity{Title="Change the soil", Instructions="Carefully remove the aloe and soil from the pot. Then fill the pot halfway with the new soil. Then plant there Aloe, cleared of the previous soil and sprinkle new soil on top. Pour some water.",NeededTools="Pot", PlantID=1},
            new PlantActivity{Title="Remove the weeds around tomatoes", Instructions="Carefully mark the area around tomato to prevent its roots from accidental damage from your actions. Then start to use your tools for loosening the earth and taking out weeds with its roots.",NeededTools="Shovel, scapula", PlantID=2},
            };
            foreach (PlantActivity pa in plantActivities)
            {
                context.PlantActivities.Add(pa);
            }
            context.SaveChanges();
        }
    }
}
