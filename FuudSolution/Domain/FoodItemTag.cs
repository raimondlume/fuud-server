namespace Domain
{
    public class FoodItemTag : DomainEntity
    {
        // TODO: rename to FoodItemWithTag
        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }

        public int FoodTagId { get; set; }
        public FoodTag FoodTag { get; set; }
    }
}