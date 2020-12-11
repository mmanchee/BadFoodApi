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
  public class PlayersController : ControllerBase
  {
    private BadFoodApiContext _db;
    private readonly IUriService uriService;

    public PlayersController(BadFoodApiContext db, IUriService uriService)
    {
      _db = db;
      this.uriService = uriService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter )
    {
      var route = Request.Path.Value;
      var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
      var query = _db.Nutritions.AsQueryable();
      query = query.Skip((validFilter.PageNumber -1 ) * validFilter.PageSize).Take(validFilter.PageSize);
      var totalRecords = await _db.Nutritions.CountAsync();
      var pagedResponse = PaginationHelper.CreatePagedResponse<Nutrition>(query.ToList(), validFilter, totalRecords, uriService, route);
      return Ok(pagedResponse);
    }

    //by Food id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var nutrition = await _db.Nutritions.Where(a => a.FoodId == id).FirstOrDefaultAsync();
      return Ok(new Response<Nutrition>(nutrition));
    }
  }
}