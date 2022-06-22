using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [TestCase(10)]
        [TestCase(3)]
        public void It_decreases_quality_by_1_and_sellin_by_1_when_sellin_is_positive(int sellIn)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "cranachan", SellIn = sellIn, Quality = 20 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Multiple(() => {
                Assert.AreEqual((sellIn-1), Items[0].SellIn);
                Assert.AreEqual(19, Items[0].Quality);
            }
            );
            app.UpdateQuality();
            Assert.Multiple(() => {
                Assert.AreEqual((sellIn-2), Items[0].SellIn);
                Assert.AreEqual(18, Items[0].Quality);
            });
        }

        [Test]
        public void It_never_makes_quality_negative()
        {
            IList<Item> Items = new List<Item> { 
                new Item { Name = "cranachan", SellIn = 10, Quality = 0 }, 
                new Item { Name = "haggis", SellIn = -1, Quality = 0 },
                new Item { Name = "lemsip", SellIn = -1, Quality = 1}
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Multiple(() => {
                Assert.AreEqual(9, Items[0].SellIn);
                Assert.AreEqual(0, Items[0].Quality);
                Assert.AreEqual(-2, Items[1].SellIn);
                Assert.AreEqual(0, Items[1].Quality);
                Assert.AreEqual(0, Items[2].Quality);
            }
            );
        }

        [TestCase(0)]
        [TestCase(-5)]
        [TestCase(-100)]
        public void It_decreases_quality_by_2_when_sellin_is_less_than_1(int sellIn)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "cranachan", SellIn = sellIn, Quality = 10 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Multiple(() => {
                Assert.AreEqual(sellIn-1, Items[0].SellIn);
                Assert.AreEqual(8, Items[0].Quality);
                }
            );
        }

        [Test, Combinatorial]
        public void It_increases_quality_for_aged_brie_as_sellin_decreases([Values(4,10,49)] int qual, [Values(5, 1)] int sellIn)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", Quality = qual, SellIn = sellIn } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Multiple(() => {
                Assert.AreEqual(sellIn-1, Items[0].SellIn);
                Assert.AreEqual(qual+1, Items[0].Quality);
                }
            );
        }

        // [Test, Combinatorial]
        // public void It_increases_quality_by_2_for_aged_brie_when_sellin_is_less_than_1([Values(4,10,48)] int qual, [Values(0, -10, -100)] int sellIn)
        // {
        //     IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = sellIn, Quality = qual } };
        //     GildedRose app = new GildedRose(Items);
        //     app.UpdateQuality();
        //     Assert.Multiple(() => {
        //         Assert.AreEqual(sellIn-1, Items[0].SellIn);
        //         Assert.AreEqual(qual+2, Items[0].Quality);
        //     }
        //     );
        // }

        // [TestCase(-1, 49)]
        // [TestCase(1, 50)]
        // public void It_does_not_increase_quality_above_50(int sellin, int qual)
        // {
        //     IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = sellin, Quality = qual } };
        //     GildedRose app = new GildedRose(Items);
        //     app.UpdateQuality();
        //     Assert.AreEqual(50, Items[0].Quality);
        // }

        // [TestCase(-10)]
        // [TestCase(0)]
        // [TestCase(10)]
        // public void It_never_alters_Sulfuras_sellin_or_quality(int sellin)
        // {
        //     IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = sellin, Quality = 80 } };
        //     GildedRose app = new GildedRose(Items);
        //     app.UpdateQuality();
        //     Assert.Multiple(() => {
        //         Assert.AreEqual(80, Items[0].Quality);
        //         Assert.AreEqual(sellin, Items[0].SellIn);
        //     });
        // }

        // [Test, Combinatorial]
        // public void It_increases_quality_by_1_for_backstage_passes_when_sellin_above_10([Values(11,20,1000)] int sellin, [Values(4,10,48)] int qual)
        // {
        //     IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellin, Quality = qual } };
        //     GildedRose app = new GildedRose(Items);
        //     app.UpdateQuality();
        //     Assert.Multiple(() => {
        //         Assert.AreEqual(sellin-1, Items[0].SellIn);
        //         Assert.AreEqual(qual+1, Items[0].Quality);
        //     });
        // } 

        // [Test, Combinatorial]
        // public void It_increases_quality_by_2_for_backstage_passes_when_sellin_between_6_and_10([Values(10,6,9)] int sellin, [Values(4,10,41)] int qual)
        // {
        //     IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellin, Quality = qual } };
        //     GildedRose app = new GildedRose(Items);
        //     app.UpdateQuality();
        //     Assert.Multiple(() => {
        //         Assert.AreEqual(sellin-1, Items[0].SellIn);
        //         Assert.AreEqual(qual+2, Items[0].Quality);
        //     });
        // } 
        
        // [Test, Combinatorial]
        // public void It_increases_quality_by_3_for_backstage_passes_when_sellin_between_1_and_5([Values(5,1,3)] int sellin, [Values(4,10,41)] int qual)
        // {
        //     IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellin, Quality = qual } };
        //     GildedRose app = new GildedRose(Items);
        //     app.UpdateQuality();
        //     Assert.Multiple(() => {
        //         Assert.AreEqual(sellin-1, Items[0].SellIn);
        //         Assert.AreEqual(qual+3, Items[0].Quality);
        //     });
        // } 

        // [Test, Combinatorial]
        // public void It_sets_quality_to_0_for_backstage_passes_when_sellin_less_than_1([Values(0,-1,-10)] int sellin, [Values(4,10,41)] int qual)
        // {
        //     IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellin, Quality = qual } };
        //     GildedRose app = new GildedRose(Items);
        //     app.UpdateQuality();
        //     Assert.Multiple(() => {
        //         Assert.AreEqual(sellin-1, Items[0].SellIn);
        //         Assert.AreEqual(0, Items[0].Quality);
        //     });
        // } 
    }
}
