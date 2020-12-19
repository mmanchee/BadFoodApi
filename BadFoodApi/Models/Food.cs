namespace BadFoodApi.Models
{
  public class Food
  {
    public int FoodId { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string SubCat { get; set; }
    public string Description { get; set; }
    public int FDCId { get; set; }
    public int Caffeine { get; set; }
    public int Egg { get; set; }
    public int Fish { get; set; }
    public int FODMAP { get; set; }
    public int Fructose { get; set; }
    public int Gluten { get; set; }
    public int Histamine { get; set; }
    public int Lactose { get; set; }
    public int Lectin { get; set; }
    public int Legume { get; set; }
    public int Nut { get; set; }
    public int Oxalte { get; set; }
    public int Salicylates { get; set; }
    public int Shellfish { get; set; }
    public int Soy { get; set; }
    public int Sulfites { get; set; }
    public int Tryamine { get; set; }
  }
}