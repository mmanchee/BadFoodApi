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

namespace BadFoodApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GamesController : ControllerBase
  {
    private BadFoodApiContext _db;
    private readonly IUriService uriService;
    public GamesController(BadFoodApiContext db, IUriService uriService)
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
  }
}