using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name == "Sulfuras, Hand of Ragnaros") return;

                if (Items[i].Name == "Aged Brie")
                {
                    if (Items[i].Quality < 50) ChangeQuality(i, 1);
                    
                    ReduceSellInByOne(i);
                
                    if (Items[i].SellIn < 0 && Items[i].Quality < 50) ChangeQuality(i, 1);
                }
                
                else if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality < 50) ChangeQuality(i, 1);

                    if (Items[i].Quality < 50)
                    {
                        if (Items[i].SellIn > 5 && Items[i].SellIn < 11) ChangeQuality(i, 1);
                        else if (Items[i].SellIn < 6) ChangeQuality(i, 2);
                    }

                    ReduceSellInByOne(i);
                
                    if (Items[i].SellIn < 0) Items[i].Quality = 0;
                }
                
                else
                {
                    if (Items[i].Quality > 0) ChangeQuality(i, -1);
                    
                    ReduceSellInByOne(i);
                
                    if (Items[i].SellIn < 0 && Items[i].Quality > 0) ChangeQuality(i, -1);
                }
            }
        }

        private void ReduceSellInByOne(int i)
        {
            Items[i].SellIn -= 1;
        }

        private void ChangeQuality(int i, int numberToChange)
        {
            Items[i].Quality += numberToChange;
        }
    }
}