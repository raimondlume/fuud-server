namespace BLL.App.DTO
{
    public class FoodItemTag
    {
        public int Id { get; set; }

        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }

        public int FoodTagId { get; set; }
        public FoodTag FoodTag { get; set; }
    }
}