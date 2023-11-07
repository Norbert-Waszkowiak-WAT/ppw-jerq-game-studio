using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Query parameters for Lobby service requests.
    /// </summary>
    public class QueryLobbiesOptions 
    {
        /// <summary>
        /// The number of results to return. Minimum: 1. Maximum: 100. Default: 10.
        /// </summary>
        public int Count { get; set; } = 10;

        /// <summary>
        /// The number of results to skip before selecting results to return. Maximum: 1000. Default: 0.
        /// </summary>
        public int Skip { get; set; } = 0;

        /// <summary>
        /// Whether a random sample of results that match the search filter should be returned.
        /// </summary>
        public bool SampleResults { get; set; } = false;

        /// <summary>
        /// A list of filters which can be used to narrow down which lobbies to return.
        /// </summary>
        public List<QueryFilter> Filters { get; set; }

        /// <summary>
        /// A list of orders which define how the results should be ordered in the response.
        /// </summary>
        public List<QueryOrder> Order { get; set; }

        /// <summary>
        /// A continuation token that can be passed to subsequent query requests to fetch the next page of results.
        /// </summary>
        public string ContinuationToken { get; set; }
    }
}