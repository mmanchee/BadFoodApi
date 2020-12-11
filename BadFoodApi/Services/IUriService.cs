using System;
using Microsoft.AspNetCore.WebUtilities;
using BadFoodApi.Filter;
namespace BadFoodApi.Services
{
  public interface IUriService
  {
    Uri GetPageUri(PaginationFilter filter, string route);
  }
  public class UriService : IUriService
  {
    private readonly string _baseUri;
    public UriService(string baseUri)
    {
      _baseUri = baseUri;
    }
    public Uri GetPageUri(PaginationFilter filter, string route)
    {
      var _endpointUri = new Uri(string.Concat(_baseUri, route));
      var modifiedUri = QueryHelpers.AddQueryString(_endpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
      modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
      return new Uri(modifiedUri);
    }
  }
}