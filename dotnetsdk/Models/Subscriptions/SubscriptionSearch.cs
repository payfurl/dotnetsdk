using System;

namespace payfurl.sdk.Models.Subscriptions
{
    public class SubscriptionSearch
    {
        public decimal? AmountGreaterThan {get;set;} 
        public decimal? AmountLessThan {get;set;}
        public DateTime? AddedAfter {get;set;}
        public DateTime? AddedBefore {get;set;}
        public string Currency {get;set;}
        public string Status {get;set;}
        public SortBy Sort {get;set;}
        public int? Limit {get;set;}
        public int? Skip {get;set;}

        public enum SortBy
        {
            None,
            Date,
            Status,
            Currency,
        }
    }
}