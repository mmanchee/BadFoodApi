using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BadFoodApi.Models;
using BadFoodApi.Services;
using BadFoodApi.Filter;
using BadFoodApi.Wrappers;
using BadFoodApi.Helpers;
using System.Collections.Generic;
using System.Text.Json;
using 

namespace BadFoodApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class IssueController : ControllerBase
  {
    private BadFoodApiContext _db;
    private readonly IUriService uriService;

    public IssueController(BadFoodApiContext db, IUriService uriService)
    {
      _db = db;
      this.uriService = uriService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter )
    {
      var route = Request.Path.Value;
      var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
      var query = _db.Issues.AsQueryable();
      query = query.Skip((validFilter.PageNumber -1 ) * validFilter.PageSize).Take(validFilter.PageSize);
      var totalRecords = await _db.Issues.CountAsync();
      var pagedResponse = PaginationHelper.CreatePagedResponse<Issue>(query.ToList(), validFilter, totalRecords, uriService, route);
      return Ok(pagedResponse);
    }

    //by Food id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var issues = await _db.Issues.Where(a => a.FoodId == id).FirstOrDefaultAsync();
      return Ok(new Response<Issue>(issues));
    }

    // get list of Good Foods
    [HttpGet("FoodList")]
    public List<Food> Get( string input )
    {
      Dictionary<string,int> userData = JsonSerializer.Deserialize<Dictionary<string,int>>(input);
      string mySqlString = "SELECT * FROM issue WHERE ";
      int num = 0;
      foreach(var kvp in userData) {
        if (kvp.Value > 0) { //kvp.Key, kvp.Value
          if (num > 0) {
            mySqlString += " AND ";
          };
          mySqlString += $"{kvp.Key} < {kvp.Value}";
          num++;
        }
      }

      List<Issue> goodFoodId = _db.Issues.FromSql(mySqlString).ToList();

      List<Food> goodList = new List<Food>();
      foreach(var item in goodFoodId) {
        goodList.Add(_db.Foods.FromSql($"SELECT * FROM food WHERE foodId = {item.FoodId}").FirstOrDefault());
      }
      return goodList;
    }
  }
}
//       List<string> data = input.Split(',').ToList();
//       List<string> DbNames = new List<string> {"Amines","Caffeine","Egg","Fish","FODMAP","Folate","Fructose","Gluten","Histamine","Lactose","Lectin","Legume","Nut","Oxalte","Peanut","Salicylates","Shellfish","Soy","Sulfites"};
      
//       List<Food> foodList = _db.Foods.ToList();
//       List<Food> goodFoods = new List<Food>();

//       foreach( var food in foodList ) {
//         var testFood = _db.Issues.Where(a => a.FoodId == food.FoodId).ToList();
//         int good = 0;
        
//         good += Int32.Parse(data[0]) <= testFood[0].Amines ? 1 : 0;
//         good += Int32.Parse(data[1]) <= testFood[0].Caffeine ? 1 : 0;
//         good += Int32.Parse(data[2]) <= testFood[0].Egg ? 1 : 0;
//         good += Int32.Parse(data[3]) <= testFood[0].Fish ? 1 : 0;
//         good += Int32.Parse(data[4]) <= testFood[0].FODMAP ? 1 : 0;
//         good += Int32.Parse(data[5]) <= testFood[0].Folate ? 1 : 0;
//         good += Int32.Parse(data[6]) <= testFood[0].Fructose ? 1 : 0;
//         good += Int32.Parse(data[7]) <= testFood[0].Gluten ? 1 : 0;
//         good += Int32.Parse(data[8]) <= testFood[0].Histamine ? 1 : 0;
//         good += Int32.Parse(data[9]) <= testFood[0].Lactose ? 1 : 0;
//         good += Int32.Parse(data[10]) <= testFood[0].Lectin ? 1 : 0;
//         good += Int32.Parse(data[11]) <= testFood[0].Legume ? 1 : 0;
//         good += Int32.Parse(data[12]) <= testFood[0].Nut ? 1 : 0;
//         good += Int32.Parse(data[13]) <= testFood[0].Oxalte ? 1 : 0;
//         good += Int32.Parse(data[14]) <= testFood[0].Peanut ? 1 : 0;
//         good += Int32.Parse(data[15]) <= testFood[0].Salicylates ? 1 : 0;
//         good += Int32.Parse(data[16]) <= testFood[0].Shellfish ? 1 : 0;
//         good += Int32.Parse(data[17]) <= testFood[0].Soy ? 1 : 0;
//         good += Int32.Parse(data[18]) <= testFood[0].Sulfites ? 1 : 0;

//         if ( good < 1 )
//         {
//           goodFoods.Add(food);
//         }
//       }
//       return goodFoods;
//     }
//   }
// }

// // userInput = (dictionary){"shellfish": 2, "caffeine": 4, soy: 2}
// // let goodFoodsArr = [];
// // let query = db.get()
// // for (each entry in userInputDictionary) {
//     // query + ".where({entryKeyName >= entryVal}) 
//     // db.get().where(shellfish >= 2).where(caffeine >= 4).where(soy >= 2)
// }
// // listOfAllFoodsFromDatabase.forEach(foodRow in db) {
//     // if (!foodRow.where(shellfish >= 2).where(caffeine >= 4).where(soy>=2))
//         // goodFoodsArr.push(foodRow.foodID)
// //}
// // goodFoodsArr: [300, 451, 23, 90892384]
// // forEach(foodId in goodFoodsArr) return foodName

// good : [1,2,3,4,5,6,7,8,9] // first list
// List1 : [2,3,4,5,6,8,9]

// good : [2,3,4,5,6,8,9]
// List2 : [1,2,3,4,7,8,9]

// good : [2,3,4,8,9]
// List3 : [1,2,4,5,6,8]

// good : [2,4,8]

// Entity for raw sql
// dynamic update

// var obj = new Dictionary<string, int> 
// {
//   ["Amines"] = 0,
//   ["Folate"] = 1,
//   ["Fish"] = 0,
//   ["Shellfish"] = 1,
//   ["hist"] = 2
// }

// var raw = JsonSerializer.Serialize(obj);

// Console.WriteLine(raw);

// var back = JsonSerializer.Deserialize<Dictionary<string,int>>(raw);

// Console.WriteLine(raw);