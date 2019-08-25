namespace PublicApi.v1.DTO
{
    public class FoodItemTag
    {
        public int Id { get; set; }

        public int FoodItemId { get; set; }

        public int FoodTagId { get; set; }
        public FoodTag FoodTag { get; set; }
    }
}