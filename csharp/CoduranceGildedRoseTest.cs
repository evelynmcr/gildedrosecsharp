using System.Collections.Generic;
using NUnit.Framework;

namespace csharp
{
    [TestFixture]
    public class CoduranceGildedRoseTest
    {
        [Test]
        public void Sulfuras_sellin_does_not_decrease()
        {
            var Items = new List<Item>{new Item{ Name = "Sulfuras, Hand of Ragnaros", SellIn = 10}};
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.That(Items[0].SellIn, Is.EqualTo(10));
        }
        
        [Test, Combinatorial]
        public void Sellin_of_items_that_are_not_sulfuras_does_decrease(
            [Values("Arancino", 
                "Backstage passes to a TAFKAL80ETC concert", 
                "Aged Brie", 
                "Mac and cheese")] string name, 
            [Values(-5, 10, 1000)] int sellin)
        {
            var Items = new List<Item>
            {
                new Item{ Name = name, SellIn = sellin},
            };
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.That(Items[0].SellIn, Is.EqualTo(sellin - 1));
        }

        [TestCase(10)]
        [TestCase(1)]
        [TestCase(500)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-20)]
        public void Sulfuras_quality_never_decreases_regardless_of_whether_quality_is_greater_than_or_less_than_zero(int quality)
        {
            var Items = new List<Item>{new Item{ Name = "Sulfuras, Hand of Ragnaros", Quality = quality}};
            var rose = new GildedRose(Items);
            rose.UpdateQuality(); 
            Assert.That(Items[0].Quality, Is.EqualTo(quality));
        }

        [Test]
        public void Items_that_are_not_Aged_Brie_or_Backstage_Passes_or_Sulfuras_decrease_in_quality_by_1_when_sellin_and_quality_greater_than_zero()
        {
            var Items = new List<Item>{new Item{ Name = "Mac and cheese", Quality = 10, SellIn = 10}, 
                new Item{ Name = "Arancino", Quality = 7, SellIn = 1}, 
                new Item{ Name = "Parmigiana", Quality = 1, SellIn = 2}};
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.Multiple(() =>
            {
                Assert.That(Items[0].Quality, Is.EqualTo(9));
                Assert.That(Items[1].Quality, Is.EqualTo(6));
                Assert.That(Items[2].Quality, Is.EqualTo(0));
            });
        }
        
        [Test]
        public void Items_that_are_not_Aged_Brie_or_Backstage_Passes_or_Sulfuras_decrease_in_quality_by_2_when_sellin_less_than_one_and_quality_greater_than_one()
        {
            var Items = new List<Item>{new Item{ Name = "Mac and cheese", Quality = 10, SellIn = -1}, 
                new Item{ Name = "Arancino", Quality = 7, SellIn = 0}, 
                new Item{ Name = "Parmigiana", Quality = 2, SellIn = -10}};
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.Multiple(() =>
            {
                Assert.That(Items[0].Quality, Is.EqualTo(8));
                Assert.That(Items[1].Quality, Is.EqualTo(5));
                Assert.That(Items[2].Quality, Is.EqualTo(0));
            });
        }

        [Test]
        public void Items_that_are_not_Aged_Brie_or_Backstage_Passes_do_not_decrease_quality_to_less_than_zero_when_sellin_less_than_one()
        {
            var Items = new List<Item>{new Item{ Name = "Mac and cheese", Quality = 1, SellIn = -1}, 
                new Item{ Name = "Arancino", Quality = 0, SellIn = 0}};
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.Multiple(() =>
            {
                Assert.That(Items[0].Quality, Is.EqualTo(0));
                Assert.That(Items[1].Quality, Is.EqualTo(0));
            });
        }

        [Test]
        public void Aged_Brie_quality_increases_by_1_if_quality_is_49()
        {
            var Items = new List<Item> { new Item { Name = "Aged Brie", Quality = 49 } };
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.That(Items[0].Quality, Is.EqualTo(50));
        }
        
        [TestCase(48)]
        [TestCase(8)]
        [TestCase(0)]
        [TestCase(-2)]
        public void Aged_Brie_quality_increases_by_2_if_quality_is_48_or_less(int qual)
        {
            var Items = new List<Item> { new Item { Name = "Aged Brie", Quality = qual } };
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.That(Items[0].Quality, Is.EqualTo(qual + 2));
        }

        [TestCase(50)]
        [TestCase(55)]
        [TestCase(1000)]
        public void Aged_Brie_quality_does_not_increase_if_quality_is_50_or_more(int quality)
        {
            var Items = new List<Item> { new Item { Name = "Aged Brie", Quality = quality } };
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.That(Items[0].Quality, Is.EqualTo(quality));
        }

        [Test, Combinatorial]
        public void Backstage_passes_quality_increases_by_2_if_quality_less_than_49_and_sellin_between_6_and_10([
            Values(1, 25, 48)] int qual, [Values(6, 8, 10)] int sellIn)
        {
            var Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = qual, SellIn = sellIn} };
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.That(Items[0].Quality, Is.EqualTo(qual + 2) );
        }

        
        [TestCase(50)]
        [TestCase(51)]
        [TestCase(100)]
        public void Backstage_passes_quality_does_not_increase_if_already_50_or_more_regardless_of_sellin(int qual)
        {
            var Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = qual, SellIn = 1} };
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.That(Items[0].Quality, Is.EqualTo(qual));
        }

        [Test]
        public void Backstage_passes_quality_increases_by_1_if_quality_is_49()
        {
            var Items = new List<Item> { new Item { 
                Name = "Backstage passes to a TAFKAL80ETC concert", 
                Quality = 49, 
                SellIn = 1 } };
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.That(Items[0].Quality, Is.EqualTo(50) );
        }

        [Test, Combinatorial]
        public void Backstage_passes_quality_set_to_0_if_sellin_0_or_less(
            [Values(0, 1, 48, 55)] int qual, [Values(-10, -1, 0)] int sellIn)
        {
            var Items = new List<Item> { new Item { 
                Name = "Backstage passes to a TAFKAL80ETC concert", 
                Quality = qual, 
                SellIn = sellIn } };
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.That(Items[0].Quality, Is.EqualTo(0) );
        }

        [Test, Combinatorial]
        public void Backstage_passes_quality_increases_by_3_if_sellin_between_1_and_5_and_qual_47_or_less(
            [Values(-1, 0, 10, 46, 47)] int qual, [Values(1, 2, 5)] int sellIn)
        {
            var Items = new List<Item> { new Item { 
                Name = "Backstage passes to a TAFKAL80ETC concert", 
                Quality = qual, 
                SellIn = sellIn } };
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.That(Items[0].Quality, Is.EqualTo(qual + 3) );
        }

        [Test, Combinatorial]
        public void Backstage_passes_quality_increases_by_1_if_sellin_11_or_more_and_quality_less_than_50(
            [Values(-1, 0, 10, 46, 47, 48, 49)] int qual, [Values(11, 12, 500)] int sellIn)
        {
            var Items = new List<Item> { new Item { 
                Name = "Backstage passes to a TAFKAL80ETC concert", 
                Quality = qual, 
                SellIn = sellIn } };
            var rose = new GildedRose(Items);
            rose.UpdateQuality();
            Assert.That(Items[0].Quality, Is.EqualTo(qual + 1) );
        }
    }
}