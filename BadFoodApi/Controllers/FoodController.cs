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

namespace BadFoodApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FoodsController : ControllerBase
  {
    private BadFoodApiContext _db;
    private readonly IUriService uriService;
    public FoodsController(BadFoodApiContext db, IUriService uriService)
    {
      _db = db;
      this.uriService = uriService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter )
    {
      var route = Request.Path.Value;
      var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
      var query = _db.Foods.AsQueryable();
      query = query.Skip((validFilter.PageNumber -1 ) * validFilter.PageSize).Take(validFilter.PageSize);
      var totalRecords = await _db.Foods.CountAsync();
      var pagedResponse = PaginationHelper.CreatePagedResponse<Food>(query.ToList(), validFilter, totalRecords, uriService, route);
      return Ok(pagedResponse);
    }

    //by Food id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var food = await _db.Foods.Where(a => a.FoodId == id).FirstOrDefaultAsync();
        return Ok(new Response<Food>(food));
    }

    //Search
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PaginationFilter filter, string Name, string Category, string SubCat)
    {
      var route = Request.Path.Value;
      var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
      var query = _db.Foods.AsQueryable();

      if (Name != null)
      {
        query = query.Where(entry => entry.Name == Name); 
      }
      if (Category != null)
      {
        query = query.Where(entry => entry.Category == Category);
      }
      if (SubCat != null)
      {
        query = query.Where(entry => entry.SubCat == SubCat);
      }

      query = query.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize);

      var fullList = await _db.Foods.CountAsync();
      var pagedResponse = PaginationHelper.CreatePagedResponse<Food>(query.ToList(), validFilter, fullList, uriService, route);

      return Ok(pagedResponse);
    }
    [HttpGet("FoodList")]
    public List<Food> GetFoods( string input )
    {
      Dictionary<string,int> userData = JsonSerializer.Deserialize<Dictionary<string,int>>(input);
      string mySqlString = "SELECT * FROM foods WHERE ";
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
      List<Food> goodFoods = _db.Foods.FromSql(mySqlString).ToList();

      return goodFoods;
    }
  }
}