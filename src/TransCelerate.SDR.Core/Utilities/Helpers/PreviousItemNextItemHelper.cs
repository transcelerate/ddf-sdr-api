using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransCelerate.SDR.Core.Entities.Study;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class PreviousItemNextItemHelper
    {
        public static List<ItemEntity> GetPreviousNextItems(List<ItemEntity> itemEntities)
        {            
            for (int i = 0; i < itemEntities.Count(); i++)
            {
                var previousItems = new List<string>();
                var nextItems = new List<string>();
                if (i == 0)
                {
                    for (int j = 1; j < itemEntities.Count(); j++)
                    {
                        nextItems.Add(itemEntities[j].itemId);
                    }
                }
                else if (i == itemEntities.Count() - 1)
                {
                    for (int j = 0; j < i; j++)
                    {
                        previousItems.Add(itemEntities[j].itemId);
                    }
                }
                else
                {
                    for (int j = 0; j < i; j++)
                    {
                        previousItems.Add(itemEntities[j].itemId);
                    }
                    for (int j = i + 1; j < itemEntities.Count(); j++)
                    {
                        nextItems.Add(itemEntities[j].itemId);
                    }
                }
                itemEntities[i].previousItemsInSequence = previousItems;
                itemEntities[i].nextItemsInSequence = nextItems;
            }
            return itemEntities;
        }
    }
}
